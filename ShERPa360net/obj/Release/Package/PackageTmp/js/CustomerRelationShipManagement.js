$(document).ready(function () {
    var pageurl = window.location.href;

    if (pageurl.indexOf("Inquiry.aspx") > 0) {
        //$("#dtpPickupTime").timepicker({
        //    showInputs: false,
        //    showMeridian: false
        //})
    }

    if (pageurl.indexOf("InquiryList.aspx") > 0) {
        //InitiateInquiryFromToDate();
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
});

function InitiateInquiryFromToDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();
    today = mm + '/' + dd + '/' + yyyy;
    if ($("#txtFromDate").val().length == 0) {
        $("#txtFromDate").val(today);
    }

    if ($("#txtToDate").val().length == 0) {
        $("#txtToDate").val(today);
    }
}

function OnLeavePincode() {
    if ($('#txtPostCode').val().length == 6) {
        if ($('#hfpincode').val() == "" && $('#txtPostCode').val() != "") {
            $('#hfpincode').val($('#txtPostCode').val());
        }
        if ($('#hfpincode').val() != $('#txtPostCode').val()) {
            $('#hfpincode').val($('#txtPostCode').val());
            $('#btnSave').attr('disabled', true);
        }
    }
}


function ValidateInquiry()
{
    var Isvalidate      = true;
    var Message         = "";
    //if ($("#txtCardNo").val().length == 0) {
    //    Message = "Please enter the Card No.";
    //    Isvalidate = false;
    //}

    if ($("#ddlLeads option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the Leads." : "Please Select the Leads.";
        Isvalidate = false;
    }

    if ($("#ddlRef option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the Reference." : "Please Select the Reference.";
        Isvalidate = false;
    }

    if ($("#ddlProduct option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the Product." : "Please Select the Product.";
        Isvalidate = false;
    }

    if ($("#ddlBrand option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the Brand." : "Please Select the Brand.";
        Isvalidate = false;
    }

    if ($("#ddlModel option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the Model." : "Please Select the Model.";
        Isvalidate = false;
    }

    if ($("#txtProblems").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Problems." : "Please enter the Problems.";
        Isvalidate = false;
    }

    if ($("#txtName").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Name." : "Please enter the Name.";
        Isvalidate = false;
    }

    if ($("#txtMobileNo").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Mobile No." : "Please enter the Mobile No.";
        Isvalidate = false;
    }

    if ($("#txtAddr1").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Address1." : "Please enter the Address1.";
        Isvalidate = false;
    }

    if ($("#txtPostCode").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Post Code." : "Please enter the Post Code.";
        Isvalidate = false;
    }

    if ($("#ddlState option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the State." : "Please Select the State.";
        Isvalidate = false;
    }
    
    if ($("#ddlCity option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the City." : "Please Select the City.";
        Isvalidate = false;
    }

    if ($("#txtPickupDt").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Pickup Date." : "Please enter the Pickup Date.";
        Isvalidate = false;
    }

    if ($("#dtpPickupTime").val().length == 0) {
        Message += Message.length > 0 ? "\n Please enter the Pickup Time." : "Please enter the Pickup Time.";
        Isvalidate = false;
    }

    if ($("#ddlPayMode option:selected").val() == "0") {
        Message += Message.length > 0 ? "\n Please Select the Payment Mode." : "Please Select the Payment Mode.";
        Isvalidate = false;
    }

    if (Isvalidate == false) {
        alert(Message);
    }
    return Isvalidate;
}


function onlyNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}