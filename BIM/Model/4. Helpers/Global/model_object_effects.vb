Public Class model_object_effects
    Implements IDisposable
    Private manager_ As DevExpress.Utils.Animation.TransitionManager

    Public Sub New(manager As DevExpress.Utils.Animation.TransitionManager, object_panel As Control)
        manager_ = manager
        manager_.StartTransition(object_panel)
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        manager_.EndTransition()
        manager_ = Nothing
        GC.SuppressFinalize(Me)
    End Sub
End Class
