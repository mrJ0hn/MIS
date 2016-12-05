Imports System.Windows
Imports System.Windows.Controls

Public Class DragDropHelper

    Public Shared ReadOnly IsDragSourceProperty As DependencyProperty = _
            DependencyProperty.RegisterAttached("IsDragSource", _
                                        GetType(Boolean), _
                                        GetType(DragDropHelper), _
                                        New UIPropertyMetadata(False, AddressOf IsDragSourceChanged))

    ' format
    Private format As DataFormat = Nothing
    ' source and target
    Private initialMousePosition As Point
    Private draggedData As Object
    Private topWindow As Window
    ' source
    Private sourceItemsControl As ItemsControl
    Private sourceItemContainer As FrameworkElement
    ' singleton
    Private Shared m_instance As DragDropHelper
    Private Shared ReadOnly Property Instance() As DragDropHelper
        Get
            If m_instance Is Nothing Then
                m_instance = New DragDropHelper()
            End If
            Return m_instance
        End Get
    End Property

    Public Shared Function GetIsDragSource(ByVal obj As DependencyObject) As Boolean
        Return CType(obj.GetValue(IsDragSourceProperty), Boolean)
    End Function

    Public Shared Sub SetIsDragSource(ByVal obj As DependencyObject, ByVal value As Boolean)
        obj.SetValue(IsDragSourceProperty, value)
    End Sub

    Private Shared Sub IsDragSourceChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim dragSource = TryCast(obj, FrameworkElement)

        If dragSource IsNot Nothing Then
            If Object.Equals(e.NewValue, True) Then
                AddHandler dragSource.PreviewMouseLeftButtonDown, AddressOf Instance.DragSource_PreviewMouseLeftButtonDown
                AddHandler dragSource.PreviewMouseLeftButtonUp, AddressOf Instance.DragSource_PreviewMouseLeftButtonUp
                AddHandler dragSource.PreviewMouseMove, AddressOf Instance.DragSource_PreviewMouseMove
            Else
                RemoveHandler dragSource.PreviewMouseLeftButtonDown, AddressOf Instance.DragSource_PreviewMouseLeftButtonDown
                RemoveHandler dragSource.PreviewMouseLeftButtonUp, AddressOf Instance.DragSource_PreviewMouseLeftButtonUp
                RemoveHandler dragSource.PreviewMouseMove, AddressOf Instance.DragSource_PreviewMouseMove
            End If
        End If
    End Sub

    'DragSource
    Private Sub DragSource_PreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim i As Int32 = -1
        Dim visual As Visual = TryCast(e.OriginalSource, Visual)
        Me.initialMousePosition = e.GetPosition(Me.topWindow)
        Dim RPItemTypeName As [String] = [String].Empty
        Dim RpItemType As Type = Nothing

        Me.topWindow = DirectCast(Utilities.FindVisualAncestor(GetType(Window), DirectCast(sender, Visual)), Window)
        Me.draggedData = DirectCast(sender, FrameworkElement).DataContext
        If Me.draggedData IsNot Nothing Then Me.format = DataFormats.GetDataFormat(GetType(Appointment).FullName)

    End Sub

    Public Sub DragSource_PreviewMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If Me.draggedData IsNot Nothing AndAlso Utilities.IsMovementBigEnough(Me.initialMousePosition, e.GetPosition(Me.topWindow)) Then
            Dim data As DataObject = New DataObject(Me.format.Name, Me.draggedData)
            Try
                Dim effects As DragDropEffects = DragDrop.DoDragDrop(DirectCast(sender, DependencyObject), data, DragDropEffects.Copy)
            Catch ex As Exception
                Throw New Exception("Error in DragDropHelper: " & ex.Message)
            Finally
                Me.draggedData = Nothing
            End Try
        End If
    End Sub

    Private Sub DragSource_PreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Me.draggedData = Nothing
    End Sub

End Class



