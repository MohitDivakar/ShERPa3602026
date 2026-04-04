$(document).ready(function () {
    var pageURL = $(location).attr("href").toString();
    if (pageURL.indexOf("/Assignment.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlModel").select2();
        $("#ContentPlaceHolder1_ddlEngineer").select2();
        $("#ContentPlaceHolder1_ddlPreScanProblem").select2();
        $(".ddlTAG").select2();
        $(".ddLFaultReported").select2();
    }

    else if (pageURL.indexOf("/PrescanningEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
    }

    else if (pageURL.indexOf("/RepairEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlPartLocation").select2();
        $(".ddlPartName").select2();
    }

    else if (pageURL.indexOf("/IREntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlIRReason").select2();
    }

    else if (pageURL.indexOf("/BEREntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlBERReason").select2();
    }

    else if (pageURL.indexOf("/FAILUREEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlFailureStage").select2();
        $(".ddlFailFault").select2();
        $(".ddlInspector").select2();
    }

    else if (pageURL.indexOf("/DispatchEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlPartLocation").select2();
        $(".ddlPartName").select2();
    }

    else if (pageURL.indexOf("/AssigmentEngineerWorkUpdate.aspx") > 0)  {
        $(".ddlObjectPart").select2();
        $(".ddlFaultObjserved").select2();
        $(".ddlFaultReason").select2();
        $(".ddlAction").select2();
        $(".ddlReasonForIR").select2();
        $("#ContentPlaceHolder1_ddlEngineer").select2();
        $("#ContentPlaceHolder1_ddlModel").select2();
    }

    else if (pageURL.indexOf("/QCAssigmentUpdate.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlModel").select2();
        $("#ContentPlaceHolder1_ddlEngineer").select2();
        $(".ddlQcResult").select2();
        $(".ddlQcFailReason").select2();
    }

    else if (pageURL.indexOf("/AssignmentQcReport.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlModel").select2();
        $("#ContentPlaceHolder1_ddlEngineer").select2();
        $("#ContentPlaceHolder1_ddlStatus").select2();
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(ApplyAutoSuggestfunctionality);
});

function ApplyAutoSuggestfunctionality() {
    var pageURL = $(location).attr("href").toString();
    if (pageURL.indexOf("/Assignment.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlModel").select2();
        $("#ContentPlaceHolder1_ddlEngineer").select2();
        $("#ContentPlaceHolder1_ddlPreScanProblem").select2();
        $(".ddlTAG").select2();
        $(".ddLFaultReported").select2();
    }

    else if (pageURL.indexOf("/PrescanningEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
    }

    else if (pageURL.indexOf("/RepairEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlPartLocation").select2();
        $(".ddlPartName").select2();
    }

    else if (pageURL.indexOf("/IREntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlIRReason").select2();
    }

    else if (pageURL.indexOf("/BEREntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlBERReason").select2();
    }

    else if (pageURL.indexOf("/FAILUREEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlFailureStage").select2();
        $(".ddlFailFault").select2();
        $(".ddlInspector").select2();
    }

    else if (pageURL.indexOf("/DispatchEntry.aspx") > 0) {
        $(".ddlModel").select2();
        $(".ddlPreScanProblem").select2();
        $(".ddlPreScanEngineer").select2();
        $(".ddlEngineer").select2();
        $(".ddlRepairTechName").select2();
        $(".ddlObjectPartDescription").select2();
        $(".ddlDamageDescription").select2();
        $(".ddlCauseDescription").select2();
        $(".ddlAction").select2();
        $(".ddlRepariTask").select2();
        $(".ddlPartLocation").select2();
        $(".ddlPartName").select2();
    }
}


function AssigmentValidation() {
    var msg            = "";
    var Isvalidate     = true;
    var notificationno = $("#ContentPlaceHolder1_txtNDSNO").val();
    var esnno = $("#ContentPlaceHolder1_txtEsnNo").val();
    if ((notificationno.length > 0 && (notificationno.length > 10 || notificationno.length <= 9) == true) || isNaN(parseInt(notificationno)) == true) {
        msg = "Notification No should be 10 Digit and Integer."
        Isvalidate = false;
    }

    if ((esnno.length > 0 && (esnno.length > 12 || esnno.length <= 11) == true)  || isNaN(parseInt(esnno)) == true)  {
        Isvalidate = false;
        if (msg.length > 0) {
            msg = msg + "\n ESN No should be 12 Digit and Integer."
        }
        else {
            msg = msg + "ESN No should be 12 Digit and Integer."
        }
    }

    if (Isvalidate == false) {
        alert(msg);
    }

    return Isvalidate;
}

function ValidatePreScanningEntry() {
    var Isvalidate   = true;
    var assignmentno = $("#txtAssignmentNo").val();
    if (assignmentno.length == 0) {
        $("#lblESNExitalert").text("ESN Notification Entry Not Available.");
        $("#lblESNExitalert").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}

//function ApplyAutoSuggestfunctionality(pagename) {
//    if (pagename == "Repair") {

//    }
//}