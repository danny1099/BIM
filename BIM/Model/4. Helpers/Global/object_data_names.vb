Public Module object_data_names
    Public Enum dop
        'Consultas de las tablas catalogo del grupo general
        spSettingsGeneralSearch_Department
        spSettingsGeneralSearch_Document
        spSettingsGeneralSearch_Cities
        spSettingsGeneralSearch_Country
        spSettingsGeneralSearch_Genre
        spSettingsGeneralSearch_Payment
        spSettingsGeneralSearch_Periodicity
        spSettingsGeneralSearch_Operator

        'Procesos para la gestión de cargos
        spEntitiesBusinessPosition_Create
        spEntitiesBusinessPosition_Edited
        spEntitiesBusinessPosition_Erased
        spEntitiesBusinessPosition_Search
        spEntitiesBusinessPosition_Viewer

        'Procesos para la gestión de canales de venta
        spEntitiesBusinessChannel_Create
        spEntitiesBusinessChannel_Edited
        spEntitiesBusinessChannel_Erased
        spEntitiesBusinessChannel_Search
        spEntitiesBusinessChannel_Viewer

        'Procesos para la gestión de agencias
        spEntitiesBusinessAgency_Create
        spEntitiesBusinessAgency_Edited
        spEntitiesBusinessAgency_Erased
        spEntitiesBusinessAgency_Search
        spEntitiesBusinessAgency_Viewer

        'Procedimiento para la gestión de distribuidores
        spEntitiesBusinessDealer_Create
        spEntitiesBusinessDealer_Edited
        spEntitiesBusinessDealer_Erased
        spEntitiesBusinessDealer_Search
        spEntitiesBusinessDealer_Viewer

        'Procedimientos para la gestión de personal
        spEntitiesBussinesPersonal_Create
        spEntitiesBussinesPersonal_Erased
        spEntitiesBussinesPersonal_Leader
        spEntitiesBussinesPersonal_Search
        spEntitiesBussinesPersonal_Viewer
    End Enum
End Module
