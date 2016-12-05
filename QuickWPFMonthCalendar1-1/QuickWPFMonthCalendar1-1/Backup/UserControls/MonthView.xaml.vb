Imports System.Globalization
Imports System.Math
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Input
Imports System.Collections.Generic

Partial Public Class MonthView

    Private _dayBackBrush As Media.Brush = Media.Brushes.White
    Private _todayBackBrush As Media.Brush = CType(Me.TryFindResource("OrangeGradientBrush"), Brush)
    Private _targetBackBrush As Media.Brush = Windows.Media.Brushes.LightSlateGray

    Friend _DisplayStartDate As Date = Date.Now.AddDays(-1 * (Date.Now.Day - 1))
    Private _DisplayMonth As Integer = _DisplayStartDate.Month
    Private _DisplayYear As Integer = _DisplayStartDate.Year
    Private _cultureInfo As New CultureInfo(CultureInfo.CurrentUICulture.LCID)
    Private sysCal As Calendar = _cultureInfo.Calendar()
    Private _monthAppointments As List(Of Appointment)

    Public Event DisplayMonthChanged(ByVal e As MonthChangedEventArgs)
    Public Event DayBoxDoubleClicked(ByVal e As NewAppointmentEventArgs)
    Public Event AppointmentDblClicked(ByVal Appointment_Id As Integer)
    Public Event AppointmentMoved(ByVal Appointment_Id As Integer, ByVal OldDay As Integer, ByVal NewDay As Integer)

    Public Property DisplayStartDate() As Date
        Get
            Return _DisplayStartDate
        End Get
        Set(ByVal value As Date)
            _DisplayStartDate = value
            _DisplayMonth = _DisplayStartDate.Month
            _DisplayYear = _DisplayStartDate.Year
        End Set
    End Property

    Friend Property MonthAppointments() As List(Of Appointment)
        Get
            Return _monthAppointments
        End Get
        Set(ByVal value As List(Of Appointment))
            _monthAppointments = value
            Call BuildCalendarUI()
        End Set
    End Property

    Private Sub MonthView_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles MyBase.Loaded
        '-- Want to have the calendar show up, even if no appoints are assigned 
        '   Note - in my own app, appointments are loaded by a backgroundWorker thread to avoid a laggy UI
        If _monthAppointments Is Nothing Then Call BuildCalendarUI()
    End Sub

    Private Sub BuildCalendarUI()
        Dim iDaysInMonth As Integer = sysCal.GetDaysInMonth(_DisplayStartDate.Year, _DisplayStartDate.Month)
        Dim iOffsetDays As Integer = CInt(System.Enum.ToObject(GetType(System.DayOfWeek), _DisplayStartDate.DayOfWeek))
        Dim iWeekCount As Integer = 0
        Dim weekRowCtrl As New WeekOfDaysControls()

        'clear the monthview of all child controls, and reset the namescope to remove all the registered names.
        MonthViewGrid.Children.Clear()
        Windows.NameScope.SetNameScope(Me, New Windows.NameScope())

        Call AddRowsToMonthGrid(iDaysInMonth, iOffsetDays)
        MonthYearLabel.Content = Microsoft.VisualBasic.MonthName(_DisplayMonth) & " " & _DisplayYear

        For i As Integer = 1 To iDaysInMonth
            If (i <> 1) AndAlso System.Math.IEEERemainder((i + iOffsetDays - 1), 7) = 0 Then
                '-- add existing weekrowcontrol to the monthgrid
                Grid.SetRow(weekRowCtrl, iWeekCount)
                MonthViewGrid.Children.Add(weekRowCtrl)
                '-- make a new weekrowcontrol
                weekRowCtrl = New WeekOfDaysControls()
                iWeekCount += 1
            End If

            '-- load each weekrow with a DayBoxControl whose label is set to day number
            Dim dayBox As New DayBoxControl()
            dayBox.Name = "DayBox" & i
            dayBox.DayNumberLabel.Content = i.ToString
            dayBox.Tag = i
            AddHandler dayBox.MouseDoubleClick, AddressOf DayBox_DoubleClick
            AddHandler dayBox.PreviewDragEnter, AddressOf DayBox_PreviewEnter
            AddHandler dayBox.PreviewDragLeave, AddressOf DayBox_PreviewLeave
            'rest the namescope of the daybox in case user drags appointment from this day to another day, then back again
            Windows.NameScope.SetNameScope(dayBox, New Windows.NameScope())
            Me.RegisterName("DayBox" & i.ToString(), dayBox)

            '-- resets the list of control-names registered with this monthview (to avoid duplicates later)
            Windows.NameScope.SetNameScope(dayBox, New Windows.NameScope())
            Me.RegisterName("DayBox" & i.ToString(), dayBox)

            '-- customize daybox for today:
            If (New Date(_DisplayYear, _DisplayMonth, i)) = Date.Today Then
                dayBox.DayLabelRowBorder.Background = _todayBackBrush
                dayBox.DayAppointmentsStack.Background = Brushes.Wheat
            End If

            '-- for design mode, add appointments to random days for show...
            If System.ComponentModel.DesignerProperties.GetIsInDesignMode(Me) Then
                If Microsoft.VisualBasic.Rnd(1) < 0.25 Then
                    dayBox.DayAppointmentsStack.Children.Add(GetDummyApt(i))
                End If
            ElseIf _monthAppointments IsNot Nothing Then
                '-- Compiler warning about unpredictable results if using i (the iterator) in lambda, the 
                '   "hint" suggests declaring another var and set equal to iterator var
                Dim iday As Integer = i
                Dim aptInDay As List(Of Appointment) = _
                    _monthAppointments.FindAll(New System.Predicate(Of Appointment)( _
                                               Function(apt As Appointment) CDate(apt.StartTime).Day = iday))
                For Each a As Appointment In aptInDay
                    Dim apt As New DayBoxAppointmentControl()
                    apt.Name = "Apt" & a.AppointmentID.ToString()
                    apt.DataContext = a
                    AddHandler apt.MouseDoubleClick, AddressOf Appointment_DoubleClick
                    dayBox.DayAppointmentsStack.Children.Add(apt)
                    dayBox.RegisterName("Apt" & a.AppointmentID.ToString(), apt)
                Next

            End If

            Grid.SetColumn(dayBox, (i - (iWeekCount * 7)) + iOffsetDays)
            weekRowCtrl.WeekRowGrid.Children.Add(dayBox)
        Next
        Grid.SetRow(weekRowCtrl, iWeekCount)
        MonthViewGrid.Children.Add(weekRowCtrl)
    End Sub

    Private Sub AddRowsToMonthGrid(ByVal DaysInMonth As Integer, ByVal OffSetDays As Integer)
        MonthViewGrid.RowDefinitions.Clear()
        Dim rowHeight As New System.Windows.GridLength(60, System.Windows.GridUnitType.Star)

        Dim EndOffSetDays As Integer = 7 - _
            (CInt(System.Enum.ToObject(GetType(System.DayOfWeek), _DisplayStartDate.AddDays(DaysInMonth - 1).DayOfWeek)) + 1)

        For i As Integer = 1 To CInt((DaysInMonth + OffSetDays + EndOffSetDays) / 7)
            Dim rowDef = New RowDefinition()
            rowDef.Height = rowHeight
            MonthViewGrid.RowDefinitions.Add(rowDef)
        Next
    End Sub

    Private Sub UpdateMonth(ByVal MonthsToAdd As Integer)
        Dim ev As New MonthChangedEventArgs()
        ev.OldDisplayStartDate = _DisplayStartDate
        Me.DisplayStartDate = _DisplayStartDate.AddMonths(MonthsToAdd)
        ev.NewDisplayStartDate = _DisplayStartDate
        RaiseEvent DisplayMonthChanged(ev)
    End Sub

#Region " UI Event Handlers "

    Private Sub MonthGoPrev_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As MouseButtonEventArgs)
        UpdateMonth(-1)
    End Sub

    Private Sub MonthGoNext_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As MouseButtonEventArgs)
        UpdateMonth(1)
    End Sub

    Private Sub Appointment_DoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.Source.GetType Is GetType(DayBoxAppointmentControl) Then
            If CType(e.Source, DayBoxAppointmentControl).Tag IsNot Nothing Then
                '-- You could put your own call to your appointment-displaying code or whatever here..
                RaiseEvent AppointmentDblClicked(CInt(CType(e.Source, DayBoxAppointmentControl).Tag))
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub DayBox_DoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        '-- call to FindVisualAncestor to make sure they didn't click on existing appointment (in which case,
        '   that appointment window is already opened by handler Appointment_DoubleClick)
        If e.Source.GetType Is GetType(DayBoxControl) AndAlso _
        Utilities.FindVisualAncestor(GetType(DayBoxAppointmentControl), e.OriginalSource) Is Nothing Then

            Dim ev As New NewAppointmentEventArgs()
            If CType(e.Source, DayBoxControl).Tag IsNot Nothing Then
                ev.StartDate = New Date(_DisplayYear, _DisplayMonth, CInt(CType(e.Source, DayBoxControl).Tag), 10, 0, 0)
                ev.EndDate = CDate(ev.StartDate).AddHours(2)
            End If
            RaiseEvent DayBoxDoubleClicked(ev)
            e.Handled = True
        End If
    End Sub

    Private Sub DayBox_PreviewEnter(ByVal sender As Object, ByVal e As Windows.DragEventArgs)
        If sender.GetType Is GetType(DayBoxControl) AndAlso e.Data.GetFormats.Contains(GetType(Appointment).FullName) Then
            DirectCast(sender, DayBoxControl).DayAppointmentsStack.Background = _targetBackBrush
            e.Handled = True
        End If
    End Sub

    Private Sub DayBox_PreviewLeave(ByVal sender As System.Object, ByVal e As System.Windows.DragEventArgs)
        If sender.GetType Is GetType(DayBoxControl) Then
            Call RestoreDayBoxBackground(sender)
        End If
    End Sub

    Private Sub MonthViewGrid_PreviewDrop(ByVal sender As Object, ByVal e As Windows.DragEventArgs)
        Dim Apt As Appointment = e.Data.GetData(GetType(Appointment).FullName, False)
        If Apt IsNot Nothing Then
            Dim DayBoxOld As DayBoxControl = MonthViewGrid.FindName("DayBox" & Apt.StartTime.Value.Day)
            'only allow drag/drop (move) for appointments already in the current displayed month
            If DayBoxOld IsNot Nothing Then
                Dim DayBoxNew As DayBoxControl = Utilities.FindVisualAncestor(GetType(DayBoxControl), e.OriginalSource)
                'only allow drag/drop (move) to days in the current displayed month
                If DayBoxNew IsNot Nothing Then
                    'find the original daybox appointment in the DayAppointmentsStack
                    Dim AptBox As DayBoxAppointmentControl = DayBoxOld.FindName("Apt" & Apt.AppointmentID.ToString())
                    If AptBox IsNot Nothing Then
                        'remove the DayBoxAppointmentControl from the original DayBoxControl and unregister its name
                        DayBoxOld.DayAppointmentsStack.Children.Remove(AptBox)
                        DayBoxOld.UnregisterName("Apt" & Apt.AppointmentID.ToString())
                        'add the DayBoxAppointmentControl to DayBoxNew, register name, and reset the bg color to white
                        DayBoxNew.DayAppointmentsStack.Children.Add(AptBox)
                        DayBoxNew.RegisterName("Apt" & Apt.AppointmentID.ToString(), AptBox)
                        Call RestoreDayBoxBackground(DayBoxNew)

                        'change the start-date and end-date of the appointment to be the date represented by DayBoxNew. *Note that 
                        'the calendar doesn't (yet) support display of multi-day apts, but I'm using an offset since eventually it will.
                        Dim MoveDays As Integer = DirectCast(DayBoxNew.Tag, Integer) - Apt.StartTime.Value.Day
                        Apt.StartTime = Apt.StartTime.Value.AddDays(MoveDays)
                        Apt.EndTime = Apt.EndTime.Value.AddDays(MoveDays)

                        'Raise the AppointmentMoved event, which your code will need to handle. Change the args to suit your taste ;-)
                        RaiseEvent AppointmentMoved(Apt.AppointmentID, DayBoxOld.Tag, Apt.StartTime.Value.Day)

                    End If

                    e.Handled = True

                End If

            End If

        End If

    End Sub

#End Region

    Private Sub RestoreDayBoxBackground(ByVal DayBox As DayBoxControl)
        If DayBox.Tag = Date.Today.Day Then
            DayBox.DayAppointmentsStack.Background = _todayBackBrush
        Else
            DayBox.DayAppointmentsStack.Background = _dayBackBrush
        End If
    End Sub

    Private Function GetDummyApt(ByVal Day As Integer) As Border
        Dim bdr As New Border()
        bdr.CornerRadius = New CornerRadius(5)
        bdr.BorderBrush = Brushes.DarkOliveGreen
        bdr.Background = Brushes.LightGreen
        bdr.Margin = New Thickness(2, 2, 2, 1)
        bdr.BorderThickness = New Thickness(1)
        bdr.Child = New TextBlock(New Run("Apt on " & Day))
        DirectCast(bdr.Child, TextBlock).Padding = New Thickness(2)
        DirectCast(bdr.Child, TextBlock).FontSize = 10
        Return bdr
    End Function
End Class

Public Structure MonthChangedEventArgs
    Public OldDisplayStartDate As Date
    Public NewDisplayStartDate As Date
End Structure

Public Structure NewAppointmentEventArgs
    Public StartDate As Date?
    Public EndDate As Date?
    Public CandidateId As Integer?
    Public RequirementId As Integer?
End Structure
