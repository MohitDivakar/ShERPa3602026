// File Upload

$(function () {
    $("#file-simple").fileinput({
        showUpload: false,
        showCaption: false,
        browseClass: "btn btn-danger",
        fileType: "any"
    });

    $(".file-simple").fileinput({
        showUpload: false,
        showCaption: false,
        browseClass: "btn btn-danger",
        fileType: "any"
    });

});


$(".datepicker").datepicker({
    format: 'dd-mm-yyyy'
});

$(".datepicker").attr("autocomplete", "off");
//active menu 



function ShowHideMenu() {
    debugger;
    var h = window.innerWidth;
    var i = screen.width;
    if (h <= 768) {

        //$("#dvmenudektop").css("display", "none");
        //$("#dvmenumobile").css("display", "block");
        //$("#dvprofile").css("display", "block");
        $("#divMob").css("width", "100px");

    }
    else if (h = 1440) {
        $("#divMob").css("width", "1300px");
    }
    else if (h >= 768 && h <= 2560) {
        $("#divMob").css("width", "950px");
        $("#btnAsk").css("width", "60px");
    }
    else {
        //$("#dvmenudektop").css("display", "block");
        //$("#dvmenumobile").css("display", "none");
        //$("#dvprofile").css("display", "none");
        $("#divMob").css("width", "1300px");
    }
}




$(document).ready(function () {
    //ShowHideMenu();




    // ACTIVE MENU
    var menutabid = $("#ContentPlaceHolderMenu_menutabid").attr("value");
    if (menutabid != null) {
        //$("#" + menutabid).addClass("active");
        $("[title=" + menutabid + "]").addClass("active");
    }

    var mainmenuid = $("#mainmenuid").attr("value");
    if (mainmenuid != null) {
        //$("#" + mainmenuid).addClass("active");
        $("[title=" + mainmenuid + "]").addClass("active");
    }


    // ACTIVE MENU

    //document.onkeydown = function () {
    //    switch (event.keyCode) {
    //        case 116: //F5 button
    //            event.returnValue = false;
    //            event.keyCode = 0;
    //            return false;
    //        case 82: //R button
    //            if (event.ctrlKey) {
    //                event.returnValue = false;
    //                event.keyCode = 0;
    //                return false;
    //            }
    //    }
    //}



    //go to top button jquery //
    if ($('#back-to-top').length) {
        var scrollTrigger = 100, // px
            backToTop = function () {
                var scrollTop = $(window).scrollTop();
                if (scrollTop > scrollTrigger) {
                    $('#back-to-top').addClass('show');
                } else {
                    $('#back-to-top').removeClass('show');
                }
            };
        backToTop();
        $(window).on('scroll', function () {
            backToTop();
        });
        $('#back-to-top').on('click', function (e) {
            e.preventDefault();
            $('html,body').animate({
                scrollTop: 0
            }, 700);
        });
    }
    //End go to top button jquery //


    // duallistbox panel view //
    $('.dual_select').bootstrapDualListbox({
        selectorMinimalHeight: 160
    });



    // End  duallistbox panel view //


});

//$("#button").click(function () {

//});

// product side panel view //
function Sildepanel(elem) {

    $(".page-content").animate({
        height: "1000px"
    }, 150); // how long the animation should be

    var a = document.getElementsByTagName('a');

    // loop through all 'a' elements
    for (i = 0; i < a.length; i++) {
        // Remove the class 'active' if it exists
        a[i].classList.remove('color_active')



    }
    // add 'active' classs to the element that was clicked
    elem.classList.add('color_active');

    var elem = document.getElementById("tableblur");


    var x = document.getElementById("product_view");
    if (x.style.display === "none") {
        x.style.display = "block";

        elem.classList.add("tableblurcss");

    } else {
        elem.classList.remove("tableblurcss");
        x.style.display = "none";
    }
}


function Sildepanel_1(elem) {

    $(".page-content").animate({
        height: "1000px"
    }, 150); // how long the animation should be

    var a = document.getElementsByTagName('a');

    // loop through all 'a' elements
    for (i = 0; i < a.length; i++) {
        // Remove the class 'active' if it exists
        a[i].classList.remove('color_active')



    }
    // add 'active' classs to the element that was clicked
    elem.classList.add('color_active');

    var elem = document.getElementById("tableblur");


    var x = document.getElementById("product_view1");
    if (x.style.display === "none") {
        x.style.display = "block";

        elem.classList.add("tableblurcss");

    } else {
        elem.classList.remove("tableblurcss");
        x.style.display = "none";
    }
}
// End product side panel view //

function CancelConfirm() {
    var Isconfirm = false;
    Isconfirm = confirm("Are you sure you wish to cancel this record?");
    return Isconfirm;
}


// data table filter dropdown by column  //


//Show in Plugins.js for this

// ENd data table filter dropdown by column  //

function AllowtoRejectAllQty() {

    document.getElementById("busy-holder1").style.display = "";
    document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";

    var rowindex = 1;
    var count = 0;
    var ItemsrejectAllqty = "";
    var Isvalidate = true;
    $("#ContentPlaceHolder1_grvListItem tr").each(function () {
        if (rowindex != 1) {
            var itemname = $("#ContentPlaceHolder1_grvListItem_lblItem_" + count.toString()).text();
            var acceptqty = $("#ContentPlaceHolder1_grvListItem_txtAcceptedQty_" + count.toString()).val();
            if (acceptqty == "" || acceptqty.length == 0 || parseFloat(acceptqty) == 0) {
                if (ItemsrejectAllqty.length == 0) {
                    ItemsrejectAllqty = "Item Name : " + itemname + " Qty : " + acceptqty;
                }
                else {
                    ItemsrejectAllqty = ItemsrejectAllqty + "\n" + "Item Name : " + itemname + " Qty : " + acceptqty;
                }
            }
            count = count + 1;
        }
        else {
            rowindex = rowindex + 1;
        }
    });

    if (ItemsrejectAllqty.length != 0) {
        var confirmmessage = "Are you sure you want to reject all Qty for Below Item?  \n " + ItemsrejectAllqty;
        Isvalidate = confirm(confirmmessage);
    }
    return Isvalidate;
}


function CheckValidDate() {
    debugger;
    var bill = $("#ContentPlaceHolder1_txtBillDate").val();
    var vend = $("#ContentPlaceHolder1_txtVendor").val();
    var name = $("#ContentPlaceHolder1_txtVendorName").val();
    var blno = $("#ContentPlaceHolder1_txtBillNo").val();
    if (bill != "" && vend != "" && name != "" && blno != "") {
        document.getElementById("busy-holder1").style.display = "";
        document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
    }


    var Isvalidate = true;
    var errormsg = "";
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    var Val_PBDate = $("#ContentPlaceHolder1_txtPBDATE").val();
    if (Val_PBDate.match(dateformat)) {
        var seperator1 = Val_PBDate.split('/');
        var seperator2 = Val_PBDate.split('-');

        if (seperator1.length > 1) {
            var splitdate = Val_PBDate.split('/');
        }
        else if (seperator2.length > 1) {
            var splitdate = Val_PBDate.split('-');
        }
        var dd = parseInt(splitdate[0]);
        var mm = parseInt(splitdate[1]);
        var yy = parseInt(splitdate[2]);
        var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        if (mm == 1 || mm > 2) {
            if (dd > ListofDays[mm - 1]) {
                errormsg = "Invalid PB Date format!";     //alert('Invalid PB Date format!');
                Isvalidate = false;
            }
        }
        if (mm == 2) {
            var lyear = false;
            if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                lyear = true;
            }
            if ((lyear == false) && (dd >= 29)) {
                errormsg = "Invalid PB Date format!";       //alert('Invalid PB Date format!');
                Isvalidate = false;
                //alert('Invalid PB Date format!');
            }
            if ((lyear == true) && (dd > 29)) {
                errormsg = "Invalid PB Date format!";       //alert('Invalid PB Date format!');
                Isvalidate = false;
            }
        }
    }
    else {
        errormsg = "Invalid PB Date format!";       //alert('Invalid PB Date format!');
        Isvalidate = false;
    }

    var Val_date = $("#ContentPlaceHolder1_txtBillDate").val();
    if (Val_date.match(dateformat)) {
        var seperator1 = Val_date.split('/');
        var seperator2 = Val_date.split('-');

        if (seperator1.length > 1) {
            var splitdate = Val_date.split('/');
        }
        else if (seperator2.length > 1) {
            var splitdate = Val_date.split('-');
        }
        var dd = parseInt(splitdate[0]);
        var mm = parseInt(splitdate[1]);
        var yy = parseInt(splitdate[2]);
        var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        if (mm == 1 || mm > 2) {
            if (dd > ListofDays[mm - 1]) {
                errormsg += (errormsg.length > 0 ? "\n Invalid Bill Date format!" : "Invalid Bill Date format!");
                Isvalidate = false;
            }
        }
        if (mm == 2) {
            var lyear = false;
            if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                lyear = true;
            }
            if ((lyear == false) && (dd >= 29)) {
                errormsg += (errormsg.length > 0 ? "\n Invalid Bill Date format!" : "Invalid Bill Date format!");
                Isvalidate = false;
            }
            if ((lyear == true) && (dd > 29)) {
                errormsg += (errormsg.length > 0 ? "\n Invalid Bill Date format!" : "Invalid Bill Date format!");
                Isvalidate = false;
            }
        }
    }
    else {
        errormsg += (errormsg.length > 0 ? "\n Invalid Bill Date format!" : "Invalid Bill Date format!");
        Isvalidate = false;
    }

    if (Isvalidate == false) {
        alert(errormsg);
    }
    return Isvalidate;
}
