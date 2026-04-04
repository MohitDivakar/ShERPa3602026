function pageLoad(sender, args) {
    var isPartialPostBack = args._isPartialLoad;
    if (isPartialPostBack) {
        pageLoad2()
    }
    pageLoad1(), pageLoad2();
};

function pageLoad1() {

    $(function () {


        //Initialize Select2 Elements
        $('.select2').select2()
        //Date picker
        $('.datepicker').datepicker({
            endDate: '-0d',
            autoclose: true,
            format: "dd-mm-yyyy"
        })

        $('.datepickerpickup').datepicker({
            //daysOfWeekDisabled: [0, 7],
            startDate: '-0d',
            endDate: '+6d',
            changeMonth: true,
            autoclose: true,
            format: "dd-mm-yyyy"
        })

        $('.datepickerpickedup').datepicker({
            startDate: '-7d',
            endDate: '-0d',
            //daysOfWeekDisabled: [0, 7],
            changeMonth: true,
            autoclose: true,
            format: "dd-mm-yyyy"
        })

        $('.datepickerfedexpickup').datepicker({
            daysOfWeekDisabled: [0, 7],
            startDate: '-0d',
            endDate: '+2d',
            changeMonth: true,
            autoclose: true,
            format: "dd-mm-yyyy"
        })

        $('input[type=radio]').iCheck({
            radioClass: 'iradio_square-blue'
        }).on('ifChanged', function (e) {
            //if (window.Page_ClientValidate('aValidationGroup') == true) {
            $(e.target).click()
            var isChecked = e.currentTarget.checked;
            if (isChecked == true) {
            }
            //}
            //else {
            //    e.currentTarget.checked = false;
            //}
        });

        $('input[type=checkbox]').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue'
        }).on('ifChanged', function (e) {
            //if (window.Page_ClientValidate('aValidationGroup') == true) {
            $(e.target).click()
            var isChecked = e.currentTarget.checked;
            if (isChecked == true) {
            }
            //}
            //else {
            //    e.currentTarget.checked = false;
            //}
        });

        //$('.timepicker').timepicker({
        //    showInputs: false,
        //    showMeridian: false
        //})

        var dt = new Date();
        var time = dt.getHours() + ":" + dt.getMinutes();

        $('.timepickerpickup, .timepickerpickedup').bind("focus keydown", function (event) {

            if (event.type == "focus") {
                // Input focused/clicked
                $(this).timepicker({
                    showInputs: false,
                    showMeridian: false,
                    defaultTime: time,
                    minuteStep: 15,

                }).timepicker("showWidget")


            }
            else if (event.type == "keydown") {
                $(this).timepicker("hideWidget")
            }
        });


        //$('.timepickerpickup').on('focus', function (event) {
        //    //return
        //    $(this).timepicker({
        //        showInputs: false,
        //        showMeridian: false,
        //        minuteStep: 1,
        //   }).timepicker("showWidget")
        //})

        //$('#daterange-btn').daterangepicker(
        //  {
        //      ranges: {
        //          'Today': [moment(), moment()],
        //          'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        //          'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        //          'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        //          'This Month': [moment().startOf('month'), moment().endOf('month')],
        //          'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        //      },
        //      startDate: moment().subtract(29, 'days'),
        //      endDate: moment()
        //  },
        //  function (start, end) {
        //      $('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
        //  }
        //)
        var d = new Date();
        var currMonth = d.getMonth();
        var currYear = d.getFullYear();
        var startDate = new Date(currYear, currMonth, 1);
        $('#ContentPlaceHolder1_txtDateRange').daterangepicker({
            locale: {
                format: 'DD-MM-YYYY'
            },
            //startDate: moment().subtract(6, 'days'),
            //startDate: startDate
        });
    });

    $("#ContentPlaceHolder1_txtServiceChg").on('keyup', function () {
        var TotEst = $("#ContentPlaceHolder1_txtTotPartEst").val();
        var ServChg = $("#ContentPlaceHolder1_txtServiceChg").val();
        var LogiChg = $("#ContentPlaceHolder1_txtLogiChg").val();
        var TotVal = 0;
        if (ServChg.length > 0 && parseFloat(ServChg) > 0) {
            TotVal = parseFloat(ServChg) + parseFloat(TotEst);
            $("#ContentPlaceHolder1_txtTotAmt").val(TotVal);
        }
        else {
            TotVal = TotEst;
            $("#ContentPlaceHolder1_txtTotAmt").val(TotVal);
        }
        if (LogiChg.length > 0 && parseFloat(LogiChg) > 0) {
            TotVal = parseFloat(LogiChg) + parseFloat(TotVal);
            $("#ContentPlaceHolder1_txtTotAmt").val(TotVal);
        }
    });

    $("#ContentPlaceHolder1_txtLogiChg").on('keyup', function () {
        var TotEst = $("#ContentPlaceHolder1_txtTotPartEst").val();
        var ServChg = $("#ContentPlaceHolder1_txtServiceChg").val();
        var LogiChg = $("#ContentPlaceHolder1_txtLogiChg").val();
        var TotVal = 0;
        if (LogiChg.length > 0 && parseFloat(LogiChg) > 0) {
            TotVal = parseFloat(LogiChg) + parseFloat(TotEst);
            $("#ContentPlaceHolder1_txtTotAmt").val(TotVal);
        }
        else {
            TotVal = TotEst;
            $("#ContentPlaceHolder1_txtTotAmt").val(TotVal);
        }
        if (ServChg.length > 0 && parseFloat(ServChg) > 0) {
            TotVal = parseFloat(ServChg) + parseFloat(TotVal);
            $("#ContentPlaceHolder1_txtTotAmt").val(TotVal);
        }
    });

    /*Scroll to top when arrow up clicked BEGIN*/
    $(window).scroll(function () {
        var height = $(window).scrollTop();
        if (height > 100) {
            $('#back2Top').fadeIn();
        } else {
            $('#back2Top').fadeOut();
        }
    });

    $(function () {
        // for bootstrap 3 use 'shown.bs.tab', for bootstrap 2 use 'shown' in the next line
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            // save the latest tab; use cookies if you like 'em better:
            localStorage.setItem('lastTab', $(this).attr('href'));
            //setTimeout(function () { localStorage.removeItem("lastTab"); }, 1000 * 20);
        });

        // go to the latest tab, if it exists:
        var lastTab = localStorage.getItem('lastTab');
        if (lastTab) {
            $('[href="' + lastTab + '"]').tab('show');
        }
    });

    $(function () {

        // We can attach the `fileselect` event to all file inputs on the page
        $(document).on('change', ':file', function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            input.trigger('fileselect', [numFiles, label]);
        });

        // We can watch for our custom `fileselect` event like this
        $(document).ready(function () {
            $(':file').on('fileselect', function (event, numFiles, label) {

                var input = $(this).parents('.input-group').find(':text'),
                    log = numFiles > 1 ? numFiles + ' files selected' : label;

                if (input.length) {
                    input.val(log);
                } else {
                    if (log) alert(log);
                }

            });
        });

        //$(document).ready(function () {
        //    $('#ContentPlaceHolder1_ddlBrand').change(function () {
        //        this.disabled = true;
        //        $('#faLoading1').show();
        //    });
        //});

    });

    // FOR CHECKBOX CHANGED EVENT
    //$(document).on("ifChecked ifUnchecked", "#chkgvAddon", function (chkgvAddon_CheckedChanged) {
    //    if (window.Page_ClientValidate('aValidationGroup') == true) {
    //        //if (this.checked) {
    //        $('#chkgvAddon').trigger("onclick", chkgvAddon_CheckedChanged);
    //        $('#chkgvAddon').off(ifChecked);
    //        //}
    //    }
    //    else { $(this).val(e.target.checked == false); }
    //});
    // FOR CHECKBOX CHANGED EVENT

    $(document).ready(function () {
        $('[id*=gvList]').prepend($("<thead></thead>").append($('[id*=gvList]').find("tr:first"))).DataTable({
            "responsive": true,
            "scrollX": true,
            "stateSave": true,
            "stateDuration": 40 * 1,
            //"order": [[0, 'desc']],
            "ordering": false,
            "dom": '<"top"lfi<"clear">>rt<"bottom"pB<"clear">>',
            buttons: [{
                extend: 'excel',
                title: 'Exported List',
                text: "Export"
            }]
        });

        $('.dt-button').addClass('btn btn-success onebtn');

        $('[id*=gvItemList]').prepend($("<thead></thead>").append($('[id*=gvItemList]').find("tr:first"))).DataTable({
            "responsive": true,
            "scrollX": true,
            "stateSave": true,
            "stateDuration": 40 * 1,
            //"order": [[0, 'desc']],
            "ordering": false,
            "dom": '<"top"lfi<"clear">>rt<"bottom"p<"clear">>',
            buttons: [{
                extend: 'excel',
                title: 'Exported List',
                text: "Export"
            }]
        });
    });



    $(document).ready(function () {
        $('.panel-collapse').on('show.bs.collapse', function () {
            $(this).siblings('.panel-heading').addClass('active');
        });

        $('.panel-collapse').on('hide.bs.collapse', function () {
            $(this).siblings('.panel-heading').removeClass('active');
        });

        $('.panel-heading').bind("focusin focusout click", function (event) {
            if (event.type == "focusin") {
                //$(this).css({
                //    "background-color": "#00a65a",
                //    "color": "#fff !important"
                //});
                // Input focused/clicked
                $(this).css("background-color", "#fff");
            }
            else if (event.type == "focusout") {
                $(this).css("background-color", "");
            }
            else if (event.type == "click") {
                $(this).css("background-color", "");
            }
        });
    });
}




$(document).ready(function () {
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

    $("#back2Top").click(function (event) {
        event.preventDefault();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });
    // ACTIVE MENU

    document.onkeydown = function () {
        switch (event.keyCode) {
            case 116: //F5 button
                event.returnValue = false;
                event.keyCode = 0;
                return false;
            case 82: //R button
                if (event.ctrlKey) {
                    event.returnValue = false;
                    event.keyCode = 0;
                    return false;
                }
        }
    }
});

function onlyNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function PreventSpace() {
    if (event.keyCode == 32) {
        return false;
    }
}

function IsAlphaNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122)) {
        return true;
    }
    return false;
}

function validateFileType(clicked_id) {
    var fileName = $('#' + clicked_id).val();
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (extFile == "jpg" || extFile == "jpeg") {
        var FileUpload = $('#' + clicked_id).attr('id');
        var FileUploadPath = FileUpload.value;
        var fileSize = $('#' + clicked_id)[0].files[0].size;
        //if (fileSize < 1048576 || fileSize > 3145728) {
        //    $('#modal-warning').modal();
        //    $('#lblAlertMsg').text('Image size must be between 1 MB to 3 MB.');
        //    event.target.value = '';
        //}
    } else {
        $('#modal-warning').modal();
        $('#lblAlertMsg').text('Only jpg files are allowed!');
        event.target.value = '';
    }
}




