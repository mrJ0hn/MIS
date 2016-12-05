Imports System.Windows

Class Utilities
    '-- Many thanks to Bea Stollnitz, on whose blog I found the original C# version of below in a drag-drop helper class... 
    Public Shared Function FindVisualAncestor( _
                ByVal ancestorType As System.Type, _
                ByVal visual As Media.Visual) As FrameworkElement

        While (visual IsNot Nothing AndAlso Not ancestorType.IsInstanceOfType(visual))
            visual = DirectCast(Media.VisualTreeHelper.GetParent(visual), Media.Visual)
        End While
        Return CType(visual, FrameworkElement)
    End Function

    Public Shared Function IsMovementBigEnough(ByVal initialMousePosition As Point, ByVal currentPosition As Point) As Boolean

        Return (Math.Abs(currentPosition.X - initialMousePosition.X) >= SystemParameters.MinimumHorizontalDragDistance _
                OrElse Math.Abs(currentPosition.Y - initialMousePosition.Y) >= SystemParameters.MinimumVerticalDragDistance)

    End Function


End Class
