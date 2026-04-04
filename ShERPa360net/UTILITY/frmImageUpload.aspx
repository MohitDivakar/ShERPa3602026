<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmImageUpload.aspx.cs" Inherits="ShERPa360net.UTILITY.frmImageUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="../js/main.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View / Upload </strong>Mobex Images</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">


                                            <label class="col-md-3 control-label">Job Id : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobid" runat="server" CssClass="form-control" MaxLength="10" OnTextChanged="txtJobid_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJobID" runat="server" ControlToValidate="txtJobid"
                                                        ErrorMessage="Please Enter Job ID" ValidationGroup="Search" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                        ControlToValidate="txtJobid" runat="server"
                                                        ErrorMessage="Only Numbers allowed in Job Id"
                                                        ValidationExpression="\d+" ValidationGroup="Search">*
                                                    </asp:RegularExpressionValidator>
                                                    <span class="focus-input100"></span>
                                                </div>
                                            </div>




                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">


                                            <label class="col-md-4 control-label">Upload Images : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <input class="form-control" id="fileButton" type="file" style="padding: 5px" multiple />
                                                    <span class="focus-input100"></span>
                                                    <input type="text" id="durl" style="display: none" />

                                                </div>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkUpload" CssClass="btn btn-success pull-left" Text="Upload Image" ToolTip="Upload Image" OnClientClick="Storage();"><i class="fa fa-upload"></i></asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<asp:LinkButton runat="server" ID="lnkSearhMR" CssClass="btn btn-success pull-left" Text="Search MR" OnClick="lnkSearhMR_Click"><i class="fa fa-search"></i></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkMobexImage" CssClass="btn btn-success pull-left" Text="Search Image" ToolTip="Search Image" OnClick="lnkMobexImage_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <label id="lblPer"></label>
                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Image  </strong></h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="p-t-34 p-b-60 respon3">
                                        <p class="l1-txt1 p-b-10 respon2">
                                            Image Preview
                                        </p>


                                        <div id="preview"></div>
                                        <div class="cd100"></div>

                                    </div>

                                </div>



                                <div id="divPreview" runat="server">
                                    <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image runat="server" ImageUrl='<%#Eval("IMAGEURL") %>' Height="250" />
                                            <%--<image src='<%#Eval("IMAGEURL") %>'></image>--%>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
















    <script>

        function previewImages() {
            debugger;
            var $preview = $('#preview').empty();
            if (this.files) $.each(this.files, readAndPreview);

            function readAndPreview(i, file) {

                if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
                    return alert(file.name + " is not an image");
                }

                var reader = new FileReader();

                $(reader).on("load", function () {
                    $preview.append($("<img/>", { src: this.result, height: 200 }));
                });

                reader.readAsDataURL(file);

            }

        }

        $('#fileButton').on("change", previewImages);
    </script>
    <script src="https://www.gstatic.com/firebasejs/5.4.1/firebase.js"></script>
    <script>
        var config = {
            apiKey: "AIzaSyDC5ugNatJ0Dh692y2ryslZYdIxm0ppFS0",  //Web API key
            authDomain: "mobex-c0ff0.firebaseapp.com",
            databaseURL: "https://mobex-c0ff0.firebaseio.com",
            storageBucket: "gs://mobex-c0ff0.appspot.com",
            messagingSenderId: "134576698930",  //  Project number
        };
        var app = firebase.initializeApp(config),
            database = app.database(),
            auth = app.auth(),
            storage = app.storage();
    </script>
    <script>
        var fileButton = document.getElementById("fileButton");
        var Count = 0;
        function Storage() {
            debugger;
            if (Page_ClientValidate()) {
                if ($("#txtJobid").val() != "") {
                    debugger;
                    $("#lnkUpload").prop('disabled', true);
                    debugger;
                    for (var i = 0; i < fileButton.files.length; i++) {
                        var file = fileButton.files[i];
                        uploadImageAsPromise(file, i);
                    }
                }


            }

        }

        function uploadImageAsPromise(file, i) {
            debugger;
            var metadata = {
                contentType: 'image/jpeg'
            };
            var storageRef = firebase.storage().ref().child('Mobex');
            var uploadTask = storageRef.child(file.name).put(file, metadata);

            uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, // or 'state_changed'
                function (snapshot) {
                    var progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
                    $("#lblPer").innerHTML = 'Upload is ' + progress + '% done';
                    console.log('Upload is ' + progress + '% done');

                    switch (snapshot.state) {
                        case firebase.storage.TaskState.PAUSED: // or 'paused'
                            console.log('Upload is paused');
                            break;
                        case firebase.storage.TaskState.RUNNING: // or 'running'
                            console.log('Upload is running');
                            break;
                    }

                }, function (error) {

                    switch (error.code) {
                        case 'storage/unauthorized':
                            break;

                        case 'storage/canceled':
                            break;

                        case 'storage/unknown':
                            break;
                    }
                }, function () {

                    uploadTask.snapshot.ref.getDownloadURL().then(function (downloadURL) {
                        console.log('File available at', downloadURL);
                        Count++;
                        Imgtotal = fileButton.files.length;
                        if (Count === Imgtotal) {
                            alert("Success");
                            location.href = '../UTILITY/frmImageUpload.aspx/InsertData';
                        }

                        var databaseRef = database.ref().child('blynk_Image');
                        var chat = { JOBID: $("#txtJobid").value, IMAGEURL: downloadURL };

                        debugger;
                        $.ajax({
                            type: "POST",
                            url: "http://localhost:56794/UTILITY/frmImageUpload.aspx/InsertData",
                            data: "{'JOBID':'" + $("#txtJobid").value + "','IMAGEURL':'" + downloadURL + "'}",
                            contentType: "application/json; charset=utf-8",
                            error: function (result) {
                                alert("Error Occured, Try Again");
                            },
                        });


                    });
                });
        };

    </script>
























</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmUtility" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtlUplMobexImg" runat="server" />

</asp:Content>
