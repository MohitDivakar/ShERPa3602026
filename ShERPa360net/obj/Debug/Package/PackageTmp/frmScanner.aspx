<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmScanner.aspx.cs" Inherits="ShERPa360net.frmScanner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>

    <%--<script src="https://cdn.jsdelivr.net/npm/@zxing/library@0.18.0/umd/index.min.js"></script>
    <script src="https://unpkg.com/@zxing/library@0.18.6/umd/zxing-browser.min.js"></script>--%>
    <script type="text/javascript" src="https://unpkg.com/@zxing/library@latest"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <video id="video" width="100%" height="auto" style="border: 1px solid black;"></video>


            <script>
                let selectedDeviceId;
                const codeReader = new ZXing.BrowserBarcodeReader();
                function startScanner() {
                    debugger;
                    codeReader.listVideoInputDevices()
                        .then((videoInputDevices) => {
                            debugger;
                            selectedDeviceId = videoInputDevices[0].deviceId;
                            codeReader.decodeFromVideoDevice(selectedDeviceId, 'video', (result, error) => {
                                if (result) {
                                    console.log('Barcode scanned: ', result.text);
                                    fetch('https://yourdomain.com/api/barcode', {
                                        method: 'POST',
                                        headers: {
                                            'Content-Type': 'application/json',
                                        },
                                        body: JSON.stringify({ barcode: result.text })
                                    })
                                        .then(response => response.json())
                                        .then(data => console.log(data));
                                }
                                if (error) {
                                    console.error(error);
                                }
                            });
                        })
                        .catch((err) => {
                            console.error('Error accessing video devices:', err);
                        });
                }
                window.onload = startScanner;
            </script>

        </div>
    </form>
</body>
</html>
