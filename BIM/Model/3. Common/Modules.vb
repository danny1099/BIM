

Public Module Modules
    Property start_home As start_initial_home = Application.OpenForms.Cast(Of Form)().FirstOrDefault(Function(x) TypeOf x Is start_initial_home)


#Region "helpers"
    Private Function search_module(control_name As String) As Control
        Return DirectCast(Activator.CreateInstance(Type.GetType("BIM." + control_name)), Control)
    End Function

    Public Sub defaults_objects(panel_ As Control)
        Try
        Catch ex As Exception
            message_text(ex.Message.ToString, MessageBoxButtons.OK)
        End Try
    End Sub

    Public Function search_home() As start_initial_home
        Return Application.OpenForms.Cast(Of Form)().FirstOrDefault(Function(x) TypeOf x Is start_initial_home)
    End Function
#End Region

#Region "methods"
    Public Sub show_option(control_name As String)
        windows_document(search_module(control_name))
    End Sub

    Public Sub show_option(control_object As Control)
        windows_document(control_object)
    End Sub

    Public Sub show_flyout(control_object As Control)
        Using fly_out As New model_object_flyout(start_home, control_object)
            fly_out.ShowDialog(start_home)
        End Using
    End Sub

    Private Sub windows_document(module_object As Control)
        If module_object IsNot Nothing Then
            module_object.Dock = DockStyle.Fill
            module_object.TabIndex = 0

            'Controla la carga del modulo al home principal utilizando efectos según este marcado
            With start_home
                .object_panel_modules.Controls.Clear()

                If My.Settings.window_effect = True Then
                    Using temp_effect As New model_object_effects(.object_component_effect, .object_panel_modules)
                        .object_panel_modules.Controls.Add(module_object)
                    End Using
                Else
                    .object_panel_modules.Controls.Add(module_object)
                End If
            End With
        End If
    End Sub

#End Region
End Module
