Imports System.Data.SqlClient
Public NotInheritable Class Connection
    Implements IDisposable

#Region "objects"
    Public Property cnn_cloud As SqlConnection
#End Region

#Region "methods"
    Public Function connection_server() As SqlConnection
        Try
            If cnn_cloud Is Nothing Then
                cnn_cloud = New SqlConnection("Data Source=localhost;Initial Catalog=BIM;User ID=SA;Password=danny1099")
                cnn_cloud.Open()
            End If
        Catch ex As Exception
        End Try

        Return cnn_cloud
    End Function

    Private Sub Dispose() Implements IDisposable.Dispose
        cnn_cloud = Nothing
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
