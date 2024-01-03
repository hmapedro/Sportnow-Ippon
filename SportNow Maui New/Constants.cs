﻿using System;
using System.Collections.Generic;
using System.IO;
using SportNow.Model;

namespace SportNow
{
    public static class Constants
    {

        public static string servername = "plataforma.ippon.pt";
        //public static string servername = "localhost";
        public static string server = servername;//+"/ipponcrn";
        public static string images_URL = DeviceInfo.Platform == DevicePlatform.Android? "http://" + server + "/upload/" : "https://" + server + "/upload/";
        //public static string server = "localhost";

        /*public static string server = "www.ippon.pt";
        public static string images_URL = "https://plataforma.ippon.pt/upload/";*/


        // URL of REST service
        public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_select_students.php" : "https://"+server+"/services/service_select_students.php";

        public static string RestUrl_Login = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_login.php" : "https://"+server+"/services/service_login.php";

        public static string RestUrl_Update_Token = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_token.php" : "https://"+server+"/services/service_update_token.php";

        public static string RestUrl_Get_Member_Info = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_member_info.php" : "https://"+server+"/services/service_get_member_info.php";

        public static string RestUrl_Get_Member_Students_Info = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_member_students_info.php" : "https://"+server+"/services/service_get_member_students_info.php";

        public static string RestUrl_Get_Member_Students_Count = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_member_students_count.php" : "https://"+server+"/services/service_get_member_students_count.php";

        public static string RestUrl_Get_Students_Dojo_ByClass_Info = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_all_students_dojo_byclass_info.php" : "https://"+server+"/services/service_get_all_students_dojo_byclass_info.php";

        public static string RestUrl_Get_Students_Dojo_Info = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_all_students_dojo_info.php" : "https://"+server+"/services/service_get_all_students_dojo_info.php";

        public static string RestUrl_Update_Member_Info = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_member_info.php" : "https://"+server+"/services/service_update_member_info.php";

        public static string RestUrl_Create_New_Member = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_create_new_member.php" : "https://" + server + "/services/service_create_new_member.php";

        public static string RestUrl_Get_Examinations = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examinations.php" : "https://"+server+"/services/service_get_examinations.php";

        public static string RestUrl_Get_Fees= DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_fees.php" : "https://"+server+"/services/service_get_fees.php";

        public static string RestUrl_Get_Current_Fees = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_current_fees.php" : "https://"+server+"/services/service_get_current_fees.php";

        public static string RestUrl_Get_Past_Fees = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_past_fees.php" : "https://"+server+"/services/service_get_past_fees.php";

        public static string RestUrl_Create_Fee = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_create_quota_new.php" : "https://"+server+"/services/service_create_quota_new.php";

        public static string RestUrl_Update_Password = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_password.php" : "https://"+server+"/services/service_update_password.php";

        public static string RestUrl_Recover_Password = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_recover_password.php" : "https://"+server+"/services/service_recover_password.php";

        public static string RestUrl_Get_FeePayment = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_fee_payment.php" : "https://"+server+"/services/service_get_fee_payment.php";

        public static string RestUrl_Get_MonthFeePayment = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_monthfee_payment.php" : "https://" + server + "/services/service_get_monthfee_payment.php";

        public static string RestUrl_Get_Invoice_byID = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_invoice_byID.php" : "https://" + server + "/services/service_get_invoice_byID.php";

        public static string RestUrl_Get_Future_Competitions = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_competitions.php" : "https://"+server+"/services/service_get_future_competitions.php";

        public static string RestUrl_Get_Future_Competitions_All = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_competitions_all.php" : "https://"+server+"/services/service_get_future_competitions_all.php";

        public static string RestUrl_Get_Future_CompetitionParticipations = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_competitionparticipations.php" : "https://"+server+"/services/service_get_future_competitionparticipations.php";

        public static string RestUrl_Get_Past_CompetitionParticipations = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_past_competitionparticipations.php" : "https://"+server+"/services/service_get_past_competitionparticipations.php";

        public static string RestUrl_Get_Competition_Call = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_competition_call.php" : "https://"+server+"/services/service_get_competition_call.php";

        public static string RestUrl_Get_CompetitionParticipation_Payment = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_competition_participation_payment.php" : "https://"+server+"/services/service_get_competition_participation_payment.php";

        public static string RestUrl_Get_CompetitionByID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_competition_byID.php" : "https://"+server+"/services/service_get_competition_byID.php";

        public static string RestUrl_Get_CompetitionByParticipationID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_competition_byParticipationID.php" : "https://"+server+"/services/service_get_competition_byParticipationID.php";

        public static string RestUrl_Update_CompetitionParticipation_Status = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_competition_participation_status.php" : "https://"+server+"/services/service_update_competition_participation_status.php";

        public static string RestUrl_Get_Future_Events_All = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_events_all.php" : "https://"+server+"/services/service_get_future_events_all.php";

        public static string RestUrl_Get_Important_Events = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_important_events.php" : "https://"+server+"/services/service_get_important_events.php";
        
        public static string RestUrl_Get_Event_byID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_event_byID.php" : "https://"+server+"/services/service_get_event_byID.php";

        public static string RestUrl_Get_Future_EventParticipations = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_eventparticipations.php" : "https://"+server+"/services/service_get_future_eventparticipations.php";

        public static string RestUrl_Get_Past_EventParticipations = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_past_eventparticipations.php" : "https://"+server+"/services/service_get_past_eventparticipations.php";

        public static string RestUrl_Create_EventParticipation = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_create_event_participation.php" : "https://"+server+"/services/service_create_event_participation.php";

        public static string RestUrl_Get_EventParticipation_Payment = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_event_participation_payment.php" : "https://"+server+"/services/service_get_event_participation_payment.php";

        public static string RestUrl_Update_EventParticipation_Status = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_event_participation_status.php" : "https://"+server+"/services/service_update_event_participation_status.php";

        public static string RestUrl_Update_EventParticipation_Numero_Acompanhantes = Device.RuntimePlatform == Device.Android ? "https://" + server + "/services/service_update_event_participation_numero_acompanhantes.php" : "https://" + server + "/services/service_update_event_participation_numero_acompanhantes.php";

        public static string RestUrl_Get_EventParticipation = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_event_participation.php" : "https://"+server+"/services/service_get_event_participation.php";

        public static string RestUrl_Get_Examination_Timing = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_examination_timing.php" : "https://" + server + "/services/service_get_examination_timing.php";

        public static string RestUrl_Get_Examination_Program_All = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examinationprogram_all.php" : "https://"+server+"/services/service_get_examinationprogram_all.php";

        public static string RestUrl_Get_Examination_Program_Techniques = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examinationprogram_techniques.php" : "https://"+server+"/services/service_get_examinationprogram_techniques.php";

        public static string RestUrl_Get_Future_Examination_Sessions = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_examination_sessions.php" : "https://"+server+"/services/service_get_future_examination_sessions.php";

        public static string RestUrl_GetExamination_SessionByID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examination_sessions_byID.php" : "https://"+server+"/services/service_get_examination_sessions_byID.php";

        public static string RestUrl_Get_Examination_Payment = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examination_payment.php" : "https://"+server+"/services/service_get_examination_payment.php";

        public static string RestUrl_GetExamination_SessionCall = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examination_session_call.php" : "https://"+server+"/services/service_get_examination_session_call.php";

        public static string RestUrl_Get_Equipments = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_equipments.php" : "https://"+server+"/services/service_get_equipments.php";

        public static string RestUrl_Get_Classes_Detail = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_classes_detail.php" : "https://"+server+"/services/service_get_classes_detail.php";

        public static string RestUrl_Get_Future_Classes_Attendances = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_future_classes_attendances.php" : "https://"+server+"/services/service_get_future_classes_attendances.php";

        public static string RestUrl_Get_Student_Class_Schedules = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_student_classes_schedules.php" : "https://"+server+"/services/service_get_student_classes_schedules.php";

        public static string RestUrl_Get_All_Class_Schedules = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_all_classes_schedules.php" : "https://"+server+"/services/service_get_all_classes_schedules.php";

        public static string RestUrl_Get_Past_Classes_AttendanceStats= DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_past_classes_attendances_stats.php" : "https://"+server+"/services/service_get_past_classes_attendances_stats.php";

        public static string RestUrl_Get_Class_Attendances = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_class_attendances.php" : "https://" + server + "/services/service_get_class_attendances.php";

        public static string RestUrl_Get_Class_Attendance_byId = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_class_attendance_byId.php" : "https://" + server + "/services/service_get_class_attendance_byId.php";

        public static string RestUrl_Get_Classes_Inactivities = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_classes_inactivity.php" : "https://"+server+"/services/service_get_classes_inactivity.php";

        public static string RestUrl_Get_All_Classes = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_all_classes.php" : "https://" + server + "/services/service_get_all_classes.php";

        public static string RestUrl_Get_Student_Absence = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_absent_students.php" : "https://" + server + "/services/service_get_absent_students.php";        

        public static string RestUrl_Create_Classe_Attendance = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_create_class_attendance.php" : "https://"+server+"/services/service_create_class_attendance.php";

        public static string RestUrl_Update_Classe_Attendance = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_class_attendance.php" : "https://"+server+"/services/service_update_class_attendance.php";

        public static string RestUrl_Update_Member_Add_to_Class = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_update_member_add_to_class.php" : "https://" + server + "/services/service_update_member_add_to_class.php";

        public static string RestUrl_Create_Classe_Evaluation = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_create_class_evaluation.php" : "https://" + server + "/services/service_create_class_evaluation.php";
        
        public static string RestUrl_Create_EquipmentOrder = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_create_equipment_order.php" : "https://"+server+"/services/service_create_equipment_order.php";

        public static string RestUrl_Create_Examination_Result = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_create_examination_result.php" : "https://"+server+"/services/service_create_examination_result.php";

        public static string RestUrl_Update_Examination_Technic_Result = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_examination_technic_result.php" : "https://"+server+"/services/service_update_examination_technic_result.php";

        public static string RestUrl_Get_Examination_Result_byID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examination_result_byID.php" : "https://"+server+"/services/service_get_examination_result_byID.php";

        public static string RestUrl_Get_Examination_Technics_Result_byID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_examination_technics_result_byID.php" : "https://"+server+"/services/service_get_examination_technics_result_byID.php";

        public static string RestUrl_Update_Examination_Result = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_examination_result.php" : "https://"+server+"/services/service_update_examination_result.php";

        public static string RestUrl_Create_MbWay_Payment = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/mb/service_create_payment_mbway_ifthenpay.php" : "https://"+server+"/services/mb/service_create_payment_mbway_ifthenpay.php";

        public static string RestUrl_Update_Payment_Status = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_update_payment_status.php" : "https://" + server + "/services/service_update_payment_status.php";

        public static string RestUrl_Get_Dojo_Info = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_all_dojos.php" : "https://"+server+"/services/service_get_all_dojos.php";

        public static string RestUrl_Get_Awards_Student = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_award_student.php" : "https://"+server+"/services/service_get_award_student.php";

        public static string RestUrl_Get_MonthFees_byDojo = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_monthfee_byDojo.php" : "https://"+server+"/services/service_get_monthfee_byDojo.php";

        public static string RestUrl_Get_MonthFees_byStudent = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_monthfee_byStudent.php" : "https://"+server+"/services/service_get_monthfee_byStudent.php";

        public static string RestUrl_Update_MonthFee_Status_byID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_monthfee_status_byID.php" : "https://"+server+"/services/service_update_monthfee_status_byID.php";

        public static string RestUrl_Update_MonthFee_Value_byID = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_update_monthfee_value_byID.php" : "https://"+server+"/services/service_update_monthfee_value_byID.php";

        public static string RestUrl_Get_MonthFee_Payment = DeviceInfo.Platform == DevicePlatform.Android ? "https://"+server+"/services/service_get_monthfee_payment.php" : "https://"+server+"/services/service_get_monthfee_payment.php";

        public static string RestUrl_Has_MonthFeesStudent = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_has_monthfee_student.php" : "https://" + server + "/services/service_has_monthfee_student.php";

        public static string RestUrl_Has_MonthFeesResponsible = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_has_monthfee_responsible.php" : "https://" + server + "/services/service_has_monthfee_responsible.php";

        public static string RestUrl_Get_Minimum_Version = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_minimum_version.php" : "https://" + server + "/services/service_get_minimum_version.php";

        public static string RestUrl_Upload_Member_Photo = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_upload_member_photo.php" : "https://" + server + "/services/service_upload_member_photo.php";

        public static string RestUrl_Delete_Member_Photo = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_delete_member_photo.php" : "https://" + server + "/services/service_delete_member_photo.php";

        public static string RestUrl_Get_Members_To_Approve = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_members_to_approve.php" : "https://" + server + "/services/service_get_members_to_approve.php";

        public static string RestUrl_Update_Member_Approved_Status = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_update_member_approved_status.php" : "https://" + server + "/services/service_update_member_approved_status.php";

        public static string RestUrl_Get_PersonalCoaches = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_personalcoaches.php" : "https://" + server + "/services/service_get_personalcoaches.php";

        public static string RestUrl_Request_Personal = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_request_personal.php" : "https://" + server + "/services/service_request_personal.php";

        public static string RestUrl_Get_WriteLog = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_write_log.php" : "https://" + server + "/services/service_write_log.php";

        public static string RestUrl_Get_Cycles = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_cycles.php" : "https://" + server + "/services/service_get_cycles.php";

        public static string RestUrl_Update_Cycle = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_update_cycle.php" : "https://" + server + "/services/service_update_cycle.php";

        public static string RestUrl_Create_Objective = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_create_objective.php" : "https://" + server + "/services/service_create_objective.php";

        public static string RestUrl_Get_Objectives_bySeason = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_objectives_bySeason.php" : "https://" + server + "/services/service_get_objectives_bySeason.php";


        public static string RestUrl_Create_ClassProgram = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_create_class_program.php" : "https://" + server + "/services/service_create_class_program.php";

        public static string RestUrl_Get_Class_Program_byId = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_class_program_byID.php" : "https://" + server + "/services/service_get_class_program_byID.php";

        //public static string RestUrl_Get_Objectives_bySeason = DeviceInfo.Platform == DevicePlatform.Android ? "https://" + server + "/services/service_get_objectives_bySeason.php" : "https://" + server + "/services/service_get_objectives_bySeason.php";



        public static double ScreenWidth = 0;//Application.Current.MainPage.Width; //DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density; //Application.Current.MainPage.Width;//
        public static double ScreenHeight = 0;//Application.Current.MainPage.Height; //DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density; //Application.Current.MainPage.Height; //DeviceDisplay.MainDisplayInfo.Height;

        public const string DatabaseFilename = "SportNowSQLite.db3";


        public static Dictionary<string, string> fee_type { get; } = new Dictionary<string, string>
        {
            { "nksl_fnkp_seguro", "Quota Anual Praticante c/Seguro - Renovação" },
            { "nksl_fnkp", "Quota Anual Praticante s/Seguro - Renovação" },
            { "inscricao_nksl_fnkp_seguro", "Quota Anual Praticante c/Seguro - Nova" },
            { "inscricao_nksl_fnkp", "Quota Anual Praticante s/Seguro - Nova" },
            { "treinador_nksl_fnkp_seguro", "Quota Anual Treinador c/Seguro - Renovação" },
            { "treinador_nksl_fnkp", "Quota Anual Treinador s/Seguro - Renovação" },
            { "treinador_inscricao_nksl_fnkp_seguro", "Quota Anual Treinador c/Seguro - Nova" },
            { "treinador_inscricao_nksl_fnkp", "Quota Anual Treinador s/Seguro - Nova" },
            { "nao_praticante", "Quota Anual Não Praticante" },
        };

        public static Dictionary<string, string> competition_type { get; } = new Dictionary<string, string>
        {
            { "torneio_interno", "Torneio Interno" },
            { "open_nacional", "Open Nacional" },
            { "open_internacional", "Open Internacional" },
            { "campeonato_oficial", "Campeonato Oficial" },
            { "campeonato_oficial_wkf", "Campeonato Oficial WKF" },
        };

        public static Dictionary<string, string> event_type { get; } = new Dictionary<string, string>
        {
            { "estagio", "Estágio" },
            { "convivio", "Convívio" },
            { "summer_camp", "Summer Camp" },
            { "formacao", "Formação" },
            { "treino", "Treino" }
        };


        public static Dictionary<string, string> dojos { get; } = new Dictionary<string, string>
        {

            { "NKSL_Alta_Lisboa", "Alta de Lisboa" },
            { "NKSL_SFRAA", "Amadora" },
            { "candeia_encantada", "Candeia Encantada" },
            { "NKSL_Carenque", "Carenque" },
            { "NKSL_Colegio_Oriente", "Colegio Oriente" },
            { "NKSL_Dafundo", "Dafundo" },
            { "NKSL_Marvila", "Marvila" },
            { "NKSL_Santa_Iria", "Santa Iria" },
            { "Honbu_Dojo", "Sporting" },
            { "NKSL_Sa", "Voz do Operário" },
        };

        public static Dictionary<string, string> grade_type { get; } = new Dictionary<string, string>
        {
            { "menos6", "Até 6 anos" },
            { "menos12", "Até 12 anos" },
            { "mais12", "Mais 12 anos" }
        };


        public static Dictionary<string, string> grades { get; } = new Dictionary<string, string>
        {
            { "10_Kyu", "10º Kyu" },
            { "9_Kyu_Kari", "9º Kyu Kari" },
            { "9_Kyu", "9º Kyu" },
            { "8_Kyu_Kari", "8º Kyu Kari" },
            { "8_Kyu", "8º Kyu" },
            { "7_Kyu", "7º Kyu" },
            { "6_Kyu_Kari", "6º Kyu Kari" },
            { "6_Kyu", "6º Kyu" },
            { "5_Kyu", "5º Kyu" },
            { "4_Kyu_Kari", "4º Kyu Kari" },
            { "4_Kyu", "4º Kyu" },
            { "3_Kyu_Kari", "3º Kyu Kari" },
            { "3_Kyu", "3º Kyu" },
            { "2_Kyu", "2º Kyu" },
            { "1_Kyu", "1º Kyu" },
            { "1_Dan", "1º Dan" },
            { "2_Dan", "2º Dan" },
            { "3_Dan", "3º Dan" },
            { "4_Dan", "4º Dan" },
            { "5_Dan", "5º Dan" }
        };

        public static List<Belt> belts { get; } = new List<Belt> {
            new Belt { gradecode = "9_Kyu_Kari", grade = "9º Kyu Kari", image = "belt_9_Kyu_Kari.png"  },
            new Belt { gradecode = "9_Kyu", grade = "9º Kyu", image = "belt_9_Kyu.png"  },
            new Belt { gradecode = "8_Kyu_Kari", grade = "8º Kyu Kari", image = "belt_8_Kyu_Kari.png"  },
            new Belt { gradecode = "8_Kyu", grade = "8º Kyu", image = "belt_8_Kyu.png"  },
            new Belt { gradecode = "7_Kyu", grade = "7º Kyu", image = "belt_7_Kyu.png"  },
            new Belt { gradecode = "6_Kyu_Kari", grade = "6º Kyu Kari", image = "belt_6_Kyu_Kari.png"  },
            new Belt { gradecode = "6_Kyu", grade = "6º Kyu", image = "belt_7_Kyu.png"  },
            new Belt { gradecode = "5_Kyu", grade = "5º Kyu", image = "belt_5_Kyu.png"  },
            new Belt { gradecode = "4_Kyu_Kari", grade = "4º Kyu Kari", image = "belt_4_Kyu_Kari.png"  },
            new Belt { gradecode = "4_Kyu", grade = "4º Kyu", image = "belt_4_Kyu.png"  },
            new Belt { gradecode = "3_Kyu_Kari", grade = "3º Kyu Kari", image = "belt_3_Kyu_Kari.png"  },
            new Belt { gradecode = "3_Kyu", grade = "3º Kyu", image = "belt_3_Kyu.png"  },
            new Belt { gradecode = "2_Kyu", grade = "2º Kyu", image = "belt_2_Kyu.png"  },
            new Belt { gradecode = "1_Kyu", grade = "1º Kyu", image = "belt_1_Kyu.png"  },
            new Belt { gradecode = "1_Dan", grade = "1º Dan", image = "belt_1_Dan.png"  },
            new Belt { gradecode = "2_Dan", grade = "2º Dan", image = "belt_2_Dan.png"  },
            new Belt { gradecode = "3_Dan", grade = "3º Dan", image = "belt_3_Dan.png"  },
            new Belt { gradecode = "4_Dan", grade = "4º Dan", image = "belt_4_Dan.png"  },
            new Belt { gradecode = "5_Dan", grade = "5º Dan", image = "belt_4_Dan.png"  }
        };

        public static Dictionary<int, string> months { get; } = new Dictionary<int, string>
        {
            { 1, "Jan" },
            { 2, "Fev" },
            { 3, "Mar" },
            { 4, "Abr" },
            { 5, "Mai" },
            { 6, "Jun" },
            { 7, "Jul" },
            { 8, "Ago" },
            { 9, "Set" },
            { 10, "Out" },
            { 11, "Nov" },
            { 12, "Dez" }
        };

        public static Dictionary<int, string> daysofWeek { get; } = new Dictionary<int, string>
        {
            { 1, "2ª feira" },
            { 2, "3ª feira" },
            { 3, "4ª feira" },
            { 4, "5ª feira" },
            { 5, "6ª feira" },
            { 6, "Sábado" },
            { 7, "Domingo" }
        };

        public static Dictionary<string, string> daysofWeekPT { get; } = new Dictionary<string, string>
        {
            { "Monday", "2ª feira" },
            { "Tuesday", "3ª feira" },
            { "Wednesday", "4ª feira" },
            { "Thursday", "5ª feira" },
            { "Friday", "6ª feira" },
            { "Saturday", "Sábado" },
            { "Sunday", "Domingo" }
        };



        public static Dictionary<string, int> daysofWeekInt { get; } = new Dictionary<string, int>
        {
            { "Monday", 0 },
            { "Tuesday", 1 },
            { "Wednesday", 2 },
            { "Thursday", 3 },
            { "Friday", 4 },
            { "Saturday", 5 },
            { "Sunday", 6 }
        };

        public static Dictionary<string, int> dayInt { get; } = new Dictionary<string, int>
        {
            { "2_feira", 0 },
            { "3_feira", 1 },
            { "4_feira", 2 },
            { "5_feira", 3 },
            { "6_feira", 4 },
            { "sabado", 5},
            { "domingo", 6 },
        };

        public static Dictionary<string, string> genders { get; } = new Dictionary<string, string>
        {
           {"none", "-"},
           {"female", "Feminino"},
           {"male", "Masculino"}
        };


        public static Dictionary<string, string> memberTypes { get; } = new Dictionary<string, string>
        {
           {"praticante", "Praticante"},
           {"nao_praticante", "Não Praticante"}
        };

        public static Dictionary<string, string> countries { get; } = new Dictionary<string, string>
        {
          {"", ""},
            {"ABU DHABI", "ABU DABI"},
            {"ADEN", "ADEN"},
            {"AFGHANISTAN", "AFEGANISTÃO"},
            {"SOUTH AFRICA", "ÁFRICA DO SUL"},
            {"SW AFRICA", "ÁFRICA SUDOESTE"},
            {"ALBANIA", "ALBÂNIA"},
            {"GERMANY", "ALEMANHA"},
            {"ANDORRA", "ANDORRA"},
            {"ANGOLA", "ANGOLA"},
            {"ANTARCTICA", "ANTÁRTICA"},
            {"ANTIGUA", "ANTÍGUA"},
            {"NETHERLANDS ANTILLES", "ANTILHAS HOLANDESAS"},
            {"SAUDI ARABIA", "ARÁBIA SAUDITA"},
            {"ALGERIA", "ARGÉLIA"},
            {"ARGENTINA", "ARGENTINA"},
            {"ARMENIA", "ARMÉNIA"},
            {"ARUBA", "ARUBA"},
            {"AUSTRALIA", "AUSTRÁLIA"},
            {"AUSTRIA", "ÁUSTRIA"},
            {"AZERBAIJAN", "AZERBAIJÃO"},
            {"BAHAMAS", "BAHAMAS"},
            {"BAHRAIN", "BAHREIN"},
            {"BANGLADESH", "BANGLADESH"},
            {"BARBADOS", "BARBADOS"},
            {"BELGIUM", "BÉLGICA"},
            {"BELIZE", "BELIZE"},
            {"DAHOMEY", "BENIM"},
            {"BENIN", "BENIN"},
            {"BERMUDA", "BERMUDAS"},
            {"BELARUS", "BIELORÚSSIA"},
            {"BOLIVIA", "BOLÍVIA"},
            {"BOSNIA", "BÓSNIA"},
            {"BOTSWANA", "BOTSUANA"},
            {"BRAZIL", "BRASIL"},
            {"BRUNEI", "BRUNEI"},
            {"BULGARIA", "BULGÁRIA"},
            {"BURKINA FASO", "BURQUINA FASO"},
            {"BURUNDI", "BURUNDI"},
            {"BHUTAN", "BUTÃO"},
            {"CAMEROON", "CAMARÕES"},
            {"CAMBODIA", "CAMBODJA"},
            {"CANADA", "CANADÁ"},
            {"QATAR", "CATAR"},
            {"KAZAKHSTAN", "CAZAQUISTÃO"},
            {"CHAD", "CHADE"},
            {"CZECHIA", "CHÉQUIA"},
            {"CHILE", "CHILE"},
            {"CHINA", "CHINA"},
            {"CYPRUS", "CHIPRE"},
            {"VATICAN CITY", "CIDADE DO VATICANO"},
            {"COLOMBIA", "COLÔMBIA"},
            {"CONGO KINSHASA", "CONGO QUINXASSA"},
            {"CONGO", "CONGO"},
            {"KOREA, SOUTH", "COREIA DO SUL"},
            {"KOREA", "COREIA"},
            {"IVORY COAST", "COSTA DO MARFIM"},
            {"COSTA RICA", "COSTA RICA"},
            {"CROATIA", "CROÁCIA"},
            {"CUBA", "CUBA"},
            {"CURACAO", "CURAÇAU"},
            {"DENMARK", "DINAMARCA"},
            {"DJIBOUTI", "DJIBUTI"},
            {"DOMINICA", "DOMINICA"},
            {"DUBAI", "DUBAI"},
            {"EGYPT", "EGIPTO"},
            {"EL SALVADOR", "EL SALVADOR"},
            {"UNITED ARAB EMIRATES", "EMIRADOS ÁRABES UNIDOS"},
            {"ECUADOR", "EQUADOR"},
            {"SLOVAKIA", "ESLOVÁQUIA"},
            {"SLOVENIA", "ESLOVÉNIA"},
            {"SPAIN", "ESPANHA"},
            {"ESTONIA", "ESTÓNIA"},
            {"ETHIOPIA", "ETIÓPIA"},
            {"USA", "EUA"},
            {"PHILIPPINES", "FILIPINAS"},
            {"FINLAND", "FINLÂNDIA"},
            {"TAIWAN", "FORMOSA"},
            {"FRANCE", "FRANÇA"},
            {"GABON", "GABÃO"},
            {"GAMBIA", "GÂMBIA"},
            {"GHANA", "GANA"},
            {"GEORGIA", "GEÓRGIA"},
            {"GIBRALTAR", "GIBRALTAR"},
            {"GREECE", "GRÉCIA"},
            {"GREENLAND", "GRONELÂNDIA"},
            {"GUADELOUPE", "GUADALUPE"},
            {"GUAM", "GUAM"},
            {"GUATEMALA", "GUATEMALA"},
            {"FRENCH GUIANA", "GUIANA FRANCESA"},
            {"GUYANA", "GUIANA"},
            {"EQUATORIAL GUINEA", "GUINÉ EQUATORIAL"},
            {"GUINEA", "GUINÉ"},
            {"HAITI", "HAITI"},
            {"HONDURAS", "HONDURAS"},
            {"HONG KONG", "HONG KONG"},
            {"HUNGARY", "HUNGRIA"},
            {"SOUTH YEMEN", "IÉMEN DO SUL"},
            {"YEMEN", "IÉMEN"},
            {"IFNI", "IFNI"},
            {"CHRISTMAS ISLAND", "ILHA DE NATAL"},
            {"CHANNEL ISLAND UK", "ILHA DO CANAL"},
            {"US PACIFIC ISLAND", "ILHA DO PACÍFICO AMERICANO"},
            {"BOUVET ISLAND", "ILHAS BOUVET"},
            {"CAYMAN ISLANDS", "ILHAS CAIMÃO"},
            {"CANARY ISLAND", "ILHAS CANÁRIAS"},
            {"COCOS (KEELING) ISLAND", "ILHAS COCOS (KEELING)"},
            {"COMORO ISLANDS", "ILHAS COMORO"},
            {"COOK ISLANDS", "ILHAS COOK"},
            {"CAPE VERDI ISLANDS", "ILHAS DE CABO VERDE"},
            {"PACIFIC ISLAND", "ILHAS DO PACÍFICO"},
            {"FALKLAND ISLANDS", "ILHAS FALKLAND"},
            {"FAEROE ISLANDS", "ILHAS FAROÉ"},
            {"FIJI", "ILHAS FIJI"},
            {"LEEWARD ISLANDS", "ILHAS LEEWARD"},
            {"MAURITIUS", "ILHAS MAURÍCIAS"},
            {"NORFOLK ISLAND", "ILHAS NORFOLK"},
            {"RYUKYU ISLANDS", "ILHAS RYUKYU"},
            {"US VIRGIN ISLANDS", "ILHAS VIRGENS AMERICANAS"},
            {"BRITISH VIRGIN ISLANDS", "ILHAS VIRGENS BRITÂNICAS"},
            {"WAKE ISLAND", "ILHAS WAKE"},
            {"INDIA", "ÍNDIA"},
            {"BRITISH WEST INDIES", "ÍNDIAS ORIENTAIS BRITÂNICAS"},
            {"WEST INDIES", "ÍNDIAS ORIENTAIS"},
            {"INDONESIA", "INDONÉSIA"},
            {"IRAN", "IRÃO"},
            {"IRAQ", "IRAQUE"},
            {"IRELAND", "IRLANDA"},
            {"ICELAND", "ISLÂNDIA"},
            {"ISRAEL", "ISRAEL"},
            {"ITALY", "ITÁLIA"},
            {"JAMAICA", "JAMAICA"},
            {"JAPAN", "JAPÃO"},
            {"JORDAN", "JORDÂNIA"},
            {"KUWAIT", "KUWAIT"},
            {"LAOS", "LAOS"},
            {"LESOTHO", "LESOTO"},
            {"LATVIA", "LETÓNIA"},
            {"LEBANON", "LÍBANO"},
            {"LIBYA", "LÍBIA"},
            {"LIECHTENSTEIN", "LIECHTENSTEIN"},
            {"LITHUANIA", "LITUÂNIA"},
            {"LUXEMBOURG", "LUXEMBURGO"},
            {"MACAO", "MACAU"},
            {"MACEDONIA", "MACEDÓNIA"},
            {"MADAGASCAR", "MADAGÁSCAR"},
            {"MALAYSIA", "MALÁSIA"},
            {"MALAWI", "MALÁUI"},
            {"MALDIVES", "MALDIVAS"},
            {"MALI", "MALI"},
            {"MALTA", "MALTA"},
            {"MOROCCO", "MARROCOS"},
            {"MARTINIQUE", "MARTINICA"},
            {"MAURITANIA", "MAURITÂNIA"},
            {"MELANESIA", "MELANÉSIA"},
            {"MEXICO", "MÉXICO"},
            {"MOZAMBIQUE", "MOÇAMBIQUE"},
            {"MOLDOVIA", "MOLDÁVIA"},
            {"MONACO", "MÓNACO"},
            {"MONGOLIA", "MONGÓLIA"},
            {"MYANAMAR", "MYANMAR"},
            {"NAMIBIA", "NAMÍBIA"},
            {"NEPAL", "NEPAL"},
            {"NICARAGUA", "NICARÁGUA"},
            {"NIGER", "NÍGER"},
            {"NIGERIA", "NIGÉRIA"},
            {"NORWAY", "NORUEGA"},
            {"NEW CALADONIA", "NOVA CALEDÓNIA"},
            {"NEW HEBRIDES", "NOVA HÉBRIDES"},
            {"NEW ZEALAND", "NOVA ZELÂNDIA"},
            {"OMAN", "OMÃ"},
            {"OTHER", "OUTRO"},
            {"NETHERLANDS", "PAÍSES BAIXOS"},
            {"PANAMA", "PANAMÁ"},
            {"PAPUA NEW GUINEA", "PAPUA NOVA GUINÉ"},
            {"PAKISTAN", "PAQUISTÃO"},
            {"PARAGUAY", "PARAGUAI"},
            {"PERU", "PERÚ"},
            {"FRENCH POLYNESIA", "POLINÉSIA FRANCESA"},
            {"POLAND", "POLÓNIA"},
            {"PUERTO RICO", "PORTO RICO"},
            {"PORTUGAL", "PORTUGAL"},
            {"KENYA", "QUÉNIA"},
            {"KYRGYZSTAN", "QUIRGUISTÃO"},
            {"UNITED KINGDOM", "REINO UNIDO"},
            {"REPUBLIC OF SOUTH AFRICA", "REPÚBLICA DA ÁFRICA DO SUL"},
            {"REPUBLIC OF BELARUS", "REPÚBLICA DA BIELORÚSSIA"},
            {"DOMINICAN REPUBLIC", "REPÚBLICA DOMINICANA"},
            {"REUNION", "REUNIÃO"},
            {"ROMANIA", "ROMÉNIA"},
            {"RWANDA", "RUANDA"},
            {"RUSSIA", "RÚSSIA"},
            {"SABAH", "SABÁ"},
            {"SPANISH SAHARA", "SAHARA ESPANHOL"},
            {"WESTERN SAHARA", "SAHARA ORIENTAL"},
            {"AMERICAN SAMOA", "SAMOA AMERICANA"},
            {"ST. LUCIA", "SANTA LÚCIA"},
            {"ST. KITTS AND NEVIS", "SÃO CRISTÓVÃO E NEVES"},
            {"SAN MARINO", "SÃO MARINHO"},
            {"SEYCHELLES", "SEICHELES"},
            {"SENEGAL", "SENEGAL"},
            {"SIERRA LEONE", "SERRA LEOA"},
            {"SERBIA", "SÉRVIA"},
            {"SINGAPORE", "SINGAPURA"},
            {"SYRIA", "SÍRIA"},
            {"SOMALILIAND", "SOMÁLIA"},
            {"SRI LANKA", "SRI LANCA"},
            {"SWAZILAND", "SUAZILÂNDIA"},
            {"SUDAN", "SUDÃO"},
            {"SWEDEN", "SUÉCIA"},
            {"SWITZERLAND", "SUÍÇA"},
            {"SURINAM", "SURINAME"},
            {"THAILAND", "TAILÂNDIA"},
            {"TAJIKISTAN", "TAJIQUISTÃO"},
            {"TANZANIA", "TANZÂNIA"},
            {"BRITISH ANTARCTICA TERRITORY", "TERRITÓRIO DA ANTÁRTIDA BRITÂNICA"},
            {"BRITISH INDIAN OCEAN TERRITORY", "TERRITÓRIO DO OCEANO ÍNDICO BRITÂNICO"},
            {"PORTUGUESE TIMOR", "TIMOR-LESTE"},
            {"TONGA", "TONGA"},
            {"TRINIDAD", "TRINDADE"},
            {"TUNISIA", "TUNÍSIA"},
            {"TURKEY", "TURQUIA"},
            {"UKRAINE", "UCRÂNIA"},
            {"UGANDA", "UGANDA"},
            {"URUGUAY", "URUGUAI"},
            {"UZBEKISTAN", "UZBEQUISTÃO"},
            {"VANUATU", "VANUATU"},
            {"VENEZUELA", "VENEZUELA"},
            {"VIETNAM", "VIETNAME"},
            {"ZAIRE", "ZAIRE"},
            {"ZAMBIA", "ZÂMBIA"},
            {"ZIMBABWE", "ZIMBABUÉ"},
            {"CANAL ZONE", "ZONA DO CANAL"},
            {"NETHERLANDS ANTILLES NEUTRAL ZONE", "ZONA NEUTRA DAS ANTILHAS HOLANDESAS"}
        };

        public static Dictionary<string, string> personalclass_type { get; } = new Dictionary<string, string>
        {
            { "personal_exame", "Preparação Exame" },
            { "personal_competicao_kata", "Competição Kata" },
            { "personal_competicao_kumite", "Competição Kumite" },
            { "personal_outro", "Outro" }
        };

        public static List<string> famousQuotes = new List<string>()
        {
            /*"O segredo da vitória está em conhecer a si mesmo e o seu adversário.",
            "Luta sempre como se fosse a última vez.",
            "Karate é como água a ferver, se você não aquece constantemente, vai ficar fria",
            "Sabias que o Karate tem origem no Japão, nomeadamente na ilha de Okinawa.",*/
            "Luta sempre como se fosse a última vez.",
            "Karate é como a água a ferver, se você não aquece constantemente, vai ficar fria.",
            "Sê mais forte do que a tua melhor desculpa.",
            "“Quem não tem coragem para arriscar não conquistará nada na vida.” – Muhammad Ali, boxeador",
            "“Se falhar na preparação, estará preparado para falhar.” – Mark Spitz, nadador",
            "“Um campeão tem medo de perder. Todo o resto tem medo de vencer.” – Billie Jean King, tenista",
            "“Obstáculos não te devem impedir. Se encontrares uma parede, não desistas. Descubre como escalá-la.” – Michael Jordan, jogador de basket",
            "“Se não tens confiança, encontrarás sempre uma maneira de não vencer.” – Carl Lewis",
            "“A excelência física vem da excelência mental.” – Clara Hughes, ciclista e patinadora",
            "“Treina como se nunca tivesses ganho e joga como se nunca tivesses perdido“ - Michael Jordan, jogador de basket",
            "“Consigo aceitar a falha, mas não consigo aceitar o não se tentar“ - Michael Jordan, jogador de basket",
            "Não pares quando estiveres cansado, pára quando estiver feito.",
            "O Karate rege-se por 5 máximas: Auto Controlo, Caráter, Esforço, Etiqueta, Sinceridade.",
            "Sabias que o karate teve origem na ilha de Okinawa no Japão no século XIV?",
            "Quando o Karate foi criado tinha sido imposta uma regra no Japão proibindo a utilização de armas e assim foi criada esta arte marcial utilizando apenas o corpo.",
            "Sabias que a palavra “karate“ siginifica “mão vazia“? “kara“ significa vazio e “te“ significa mão.",
            "Ao longo da história do karate, diferentes mestres desenvolveram as suas próprias técnicas de autodefesa resultando assim em diferentes estilos desta arte marcial.",
            "Os principais estilos de karate são: Shotokan, Shotokai, Gojo-Ryu, Shito-Ryu e Wado-Ryu.",
            "Há duas vertentes principais no karate: o Kata e o Kumite.",
            "Num combate de karate existem 3 pontuações possíveis: Yuko - 1 ponto, Waza-ari - 2 pontos e Ippon - 3 pontos.",
            "Sabias que na avaliação da prova de kata numa competição se atribui 70% ao desempenho técnico do atleta e 30% ao desempenho físico?",
            "Decora os números em japonês: ichi, ni, san, shi, go, roku, shichi, hachi, kyu, ju :D",
            "Sabias que o kimono de karate tem o nome de Karate-Gi? Ele deve ser totalmente branco.",
            "Sabias que as graduações de karate até cinto castanho se chamam kyus e a partir de cinto negro se chamam dans?",
            "Sabias que o cinto branco é o primeiro a ser utilizado por um karateca iniciante que significa ”a pureza do iniciante”?",
            "O que importa é a jornada e não o destino.",
        };

        public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val)
        {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict)
            {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }
    }
}
