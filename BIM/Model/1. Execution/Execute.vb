Imports System.Data.SqlClient

Public Class Execute
    Implements IDisposable

#Region "objects"
    Property sql_adapter As SqlDataAdapter
    Property sql_command As SqlCommand
#End Region

#Region "methods"
    Public Sub New()
        sql_command = New SqlCommand
    End Sub

    Public Function execute_procedure(Optional show_message As Boolean = True) As Object
        Dim process_executed As New Object

        Try
            Using sql_connection As New Connection
                With sql_connection
                    If .connection_server IsNot Nothing Then
                        sql_command.Connection = .cnn_cloud
                        sql_adapter = New SqlDataAdapter(sql_command)
                        sql_adapter.SelectCommand.ExecuteNonQuery()

                        'Comprueba si el proceso a ejecutar tiene el parametro de retorno (Id) que sea diferente a cero (0)
                        process_executed = If(sql_command.Parameters("@text_message").Value.ToString.Contains("exitosamente"), True, False)

                        'Comprueba que el parametro de mensaje retorno no sea nulo o vacío 
                        If show_message = True Then
                            message_text(sql_command.Parameters("@text_message").Value.ToString, MessageBoxButtons.OK)
                        End If
                    Else
                        message_text("No se pudo establecer conexión al servidor", MessageBoxButtons.OK)
                    End If
                End With
            End Using

        Catch ex As Exception
            message_text(ex.Message.ToString, MessageBoxButtons.OK)
        Finally
            If Not sql_adapter Is Nothing Then sql_adapter.Dispose()
            If Not sql_command Is Nothing Then sql_command.Dispose()
        End Try

        Return process_executed
    End Function

    Public Function execute_function() As Object
        Dim process_executed As New Object

        Try
            Using sql_connection As New Connection
                With sql_connection
                    If .connection_server IsNot Nothing Then
                        sql_command.Connection = .cnn_cloud
                        process_executed = sql_command.ExecuteScalar
                    Else
                        message_text("No se pudo establecer conexión al servidor", MessageBoxButtons.OK)
                    End If
                End With
            End Using

        Catch ex As Exception
            message_text("No se pudo establecer conexión al servidor", MessageBoxButtons.OK)
        Finally
            If Not sql_command Is Nothing Then sql_command.Dispose()
        End Try

        Return process_executed
    End Function

    Public Function execute_table(Optional table_name_ As String = "table_source", Optional show_message As Boolean = False) As DataTable
        Dim process_executed As New DataTable With {.TableName = table_name_}

        Try
            Using sql_connection As New Connection
                With sql_connection
                    If .connection_server IsNot Nothing Then
                        sql_command.Connection = .cnn_cloud
                        sql_adapter = New SqlDataAdapter(sql_command)
                        sql_adapter.Fill(process_executed)

                        'Comprueba que el procedimiento tiene parametro de mensaje retorno
                        If sql_command.Parameters.Contains("@text_message") Then
                            If show_message = True Then If sql_command.Parameters("@text_message").Value.ToString <> "" Then message_text(sql_command.Parameters("@text_message").Value.ToString, MessageBoxButtons.OK)
                        End If
                    Else
                        message_text("No se pudo establecer conexión al servidor", MessageBoxButtons.OK)
                    End If
                End With
            End Using

        Catch ex As Exception
            message_text("No se pudo establecer conexión al servidor", MessageBoxButtons.OK)
        Finally
            If Not sql_adapter Is Nothing Then sql_adapter.Dispose()
            If Not sql_command Is Nothing Then sql_command.Dispose()
        End Try

        Return process_executed
    End Function

    Private Sub Dispose() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
