<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="BDODashboard.aspx.cs" Inherits="ShERPa360net.UTILITY.BDODashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DashBoard</title>
    <meta name="viewport" content="width=device-width , initial-scale =1.0" />
   
		
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
    <link href='https://unpkg.com/boxicons@2.0.7/css/boxicons.min.css' rel='stylesheet'>
		<link href="../Dashboardcss/style.bundle.css" rel="stylesheet" type="text/css" />
		<link href="../Dashboardcss/Dashboard.css" rel="stylesheet" type="text/css" />
	
    <style >
        #SvgjsPath1122, #SvgjsPath1120, #SvgjsPath1124, #SvgjsPath1126, #SvgjsPath1128, #SvgjsPath1130 {
    fill: #f1416c0d;
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

       
        <div class="container-fluid pt-5 pb-5">
			
            

                 <div class ="row">
                      <div class="col-md-3 cards">
					 <div class="card radius-10 border-start border-0 border-3 border-danger">
						<div class="card-body">
							<div class="d-flex align-items-center">
								<div>
									<p class="mb-0 text-secondary">Safty Report</p>
									
								
								</div>
								<div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto"><i class="bx bxs-cart"></i>
								</div>
							</div>
						</div>
					 </div>
				   </div>
                     <div class="col-md-3 cards">
					 <div class="card radius-10 border-start border-0 border-3 border-danger">
						<div class="card-body">
							<div class="d-flex align-items-center">
								<div>
									<p class="mb-0 text-secondary">Complain Report</p>
									
								
								</div>
								<div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto"><i class="bx bxs-wallet"></i>
								</div>
							</div>
						</div>
					 </div>
				   </div>
                     <div class="col-md-3 cards">
					 <div class="card radius-10 border-start border-0 border-3 border-danger">
						<div class="card-body">
							<div class="d-flex align-items-center">
								<div>
									<p class="mb-0 text-secondary">List Report</p>
									
								
								</div>
								<div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness  text-white ms-auto"><i class="bx bxs-bar-chart-alt-2"></i>
								</div>
							</div>
						</div>
					 </div>
				   </div>
                     <div class="col-md-3 cards">
					 <div class="card radius-10 border-start border-0 border-3 border-danger">
						<div class="card-body">
							<div class="d-flex align-items-center">
								<div>
									<p class="mb-0 text-secondary">Product Report</p>
									
								
								</div>
								<div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto"><i class="bx bxs-cart"></i>
								</div>
							</div>
						</div>
					 </div>
				   </div>

            </div>

                   <div class="row">
                       
                       <div class="col-md-4">
                           <div id="demo">

    <div class="row">
      <div class="card pt-5  border-top">

        <div class="customNavigation"> 
			
			<h1 class="float-left line-h2 pt-10">Today</h1>
			<a class="btn prev"><i class='bx bxs-left-arrow' ></i></a>
			<a class="btn next"><i class='bx bxs-right-arrow'></i></a> </div>
        <div id="owl-demo" class="owl-carousel">
                <div class="item ">	
			<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
               <div class="item">	
					<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
			<div class="item">	
				<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
			<div class="item">	
					<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
               
        </div>
      </div>
    </div>
 
</div>
                           
                            <br />

                      <div id="demo1">
 
    <div class="row">
      <div class="card pt-5  border-top">

        <div class="customNavigation"> 
			
			<h1 class="float-left line-h2 pt-10">OverAll</h1>
			<a class="btn prev"><i class='bx bxs-left-arrow' ></i></a>
			<a class="btn next"><i class='bx bxs-right-arrow'></i></a> </div>
        <div id="owl-demo1" class="owl-carousel">
                <div class="item">	
			<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
               <div class="item">	
					<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
			<div class="item">	
				<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
			<div class="item">	
					<div class=" bgi-no-repeat" style="background-position: right top; background-size: 30% auto; background-image: url(/metronic8/demo20/assets/media/svg/shapes/abstract-4.svg">
										<!--begin::Body-->
										<div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class=""><i class="bx bxs-cart icn-blue sz-50"></i>

                                                    </div></div>
                                                 <div class="col-md-4">
                                                     	<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-4">Total List</a>
											
											<p class="text-dark-75 fw-semibold fs-5 m-0">456898
											</p>
                                                 </div>
                                            </div>
										
										</div>
										<!--end::Body-->
									</div>


                </div>
               
        </div>
      </div>
    </div>
 
</div>
                      
                       </div>

                  
                      <div class="col-xl-8">
									<!--begin::Chart widget 18-->
									<div class="card card-flush h-xl-100 scrol-x">
										<!--begin::Header-->
										<div class="card-header pt-7">
											<!--begin::Title-->
											
                                              <h2 class="line-h2 headline pb-3 pt-3">Activity Chart</h2>
											<!--end::Title-->
											
										</div>
										<!--end::Header-->
										<!--begin::Body-->
										<div class="card-body d-flex align-items-end px-0 pt-3 pb-5">
											<!--begin::Chart-->
											<div id="kt_charts_widget_18_chart" class="h-325px w-100 min-h-auto ps-4 pe-6" style="min-height: 340px;"><div id="apexcharts9es9h62y" class="apexcharts-canvas apexcharts9es9h62y apexcharts-theme-light" style="width: 796.5px; height: 325px;"><svg id="SvgjsSvg1024" width="796.5" height="325" xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.dev" class="apexcharts-svg" xmlns:data="ApexChartsNS" transform="translate(0, 0)" style="background: transparent;"><g id="SvgjsG1026" class="apexcharts-inner apexcharts-graphical" transform="translate(55.7989501953125, 30)"><defs id="SvgjsDefs1025"><linearGradient id="SvgjsLinearGradient1030" x1="0" y1="0" x2="0" y2="1"><stop id="SvgjsStop1031" stop-opacity="0" stop-color="rgba(216,227,240,0)" offset="0"></stop><stop id="SvgjsStop1032" stop-opacity="0" stop-color="rgba(190,209,230,0)" offset="1"></stop><stop id="SvgjsStop1033" stop-opacity="0" stop-color="rgba(190,209,230,0)" offset="1"></stop></linearGradient><clipPath id="gridRectMask9es9h62y"><rect id="SvgjsRect1035" width="736.7010498046875" height="251.25799999999998" x="-3" y="-1" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect></clipPath><clipPath id="forecastMask9es9h62y"></clipPath><clipPath id="nonForecastMask9es9h62y"></clipPath><clipPath id="gridRectMarkerMask9es9h62y"><rect id="SvgjsRect1036" width="734.7010498046875" height="253.25799999999998" x="-2" y="-2" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect></clipPath></defs><rect id="SvgjsRect1034" width="29.2280419921875" height="249.25799999999998" x="0" y="0" rx="0" ry="0" opacity="1" stroke-width="0" stroke-dasharray="3" fill="url(#SvgjsLinearGradient1030)" class="apexcharts-xcrosshairs" y2="249.25799999999998" filter="none" fill-opacity="0.9"></rect><g id="SvgjsG1083" class="apexcharts-xaxis" transform="translate(0, 0)"><g id="SvgjsG1084" class="apexcharts-xaxis-texts-g" transform="translate(0, -4)"><text id="SvgjsText1086" font-family="inherit" x="52.19293212890625" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1087">QA Analysis</tspan><title>QA Analysis</title></text><text id="SvgjsText1089" font-family="inherit" x="156.57879638671875" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1090">Marketing</tspan><title>Marketing</title></text><text id="SvgjsText1092" font-family="inherit" x="260.96466064453125" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1093">Web Dev</tspan><title>Web Dev</title></text><text id="SvgjsText1095" font-family="inherit" x="365.35052490234375" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1096">Maths</tspan><title>Maths</title></text><text id="SvgjsText1098" font-family="inherit" x="469.73638916015625" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1099">Front-end Dev</tspan><title>Front-end Dev</title></text><text id="SvgjsText1101" font-family="inherit" x="574.1222534179688" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1102">Physics</tspan><title>Physics</title></text><text id="SvgjsText1104" font-family="inherit" x="678.5081176757812" y="278.258" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-xaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1105">Phylosophy</tspan><title>Phylosophy</title></text></g></g><g id="SvgjsG1123" class="apexcharts-grid"><g id="SvgjsG1124" class="apexcharts-gridlines-horizontal"><line id="SvgjsLine1126" x1="0" y1="0" x2="730.7010498046875" y2="0" stroke="#e4e6ef" stroke-dasharray="4" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1127" x1="0" y1="62.314499999999995" x2="730.7010498046875" y2="62.314499999999995" stroke="#e4e6ef" stroke-dasharray="4" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1128" x1="0" y1="124.62899999999999" x2="730.7010498046875" y2="124.62899999999999" stroke="#e4e6ef" stroke-dasharray="4" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1129" x1="0" y1="186.94349999999997" x2="730.7010498046875" y2="186.94349999999997" stroke="#e4e6ef" stroke-dasharray="4" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1130" x1="0" y1="249.25799999999998" x2="730.7010498046875" y2="249.25799999999998" stroke="#e4e6ef" stroke-dasharray="4" stroke-linecap="butt" class="apexcharts-gridline"></line></g><g id="SvgjsG1125" class="apexcharts-gridlines-vertical"></g><line id="SvgjsLine1132" x1="0" y1="249.25799999999998" x2="730.7010498046875" y2="249.25799999999998" stroke="transparent" stroke-dasharray="0" stroke-linecap="butt"></line><line id="SvgjsLine1131" x1="0" y1="1" x2="0" y2="249.25799999999998" stroke="transparent" stroke-dasharray="0" stroke-linecap="butt"></line></g><g id="SvgjsG1037" class="apexcharts-bar-series apexcharts-plot-series"><g id="SvgjsG1038" class="apexcharts-series" rel="1" seriesName="Spentxtime" data:realIndex="0"><path id="SvgjsPath1042" d="M37.5789111328125 249.25799999999998L37.5789111328125 142.0919C37.5789111328125 138.7585666666667 39.245577799479165 137.0919 42.5789111328125 137.0919L59.80695312500001 137.0919C63.140286458333335 137.0919 64.806953125 138.7585666666667 64.806953125 142.0919L64.806953125 142.0919L64.806953125 249.25799999999998L64.806953125 249.25799999999998C64.806953125 249.25799999999998 37.5789111328125 249.25799999999998 37.5789111328125 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 37.5789111328125 249.25799999999998L 37.5789111328125 142.0919Q 37.5789111328125 137.0919 42.5789111328125 137.0919L 59.80695312500001 137.0919Q 64.806953125 137.0919 64.806953125 142.0919L 64.806953125 142.0919L 64.806953125 249.25799999999998L 64.806953125 249.25799999999998z" pathFrom="M 37.5789111328125 249.25799999999998L 37.5789111328125 249.25799999999998L 64.806953125 249.25799999999998L 64.806953125 249.25799999999998L 64.806953125 249.25799999999998L 64.806953125 249.25799999999998L 64.806953125 249.25799999999998L 37.5789111328125 249.25799999999998" cy="137.0919" cx="140.964775390625" j="0" val="54" barHeight="112.16609999999999" barWidth="29.2280419921875"></path><path id="SvgjsPath1048" d="M141.964775390625 249.25799999999998L141.964775390625 167.0177C141.964775390625 163.68436666666665 143.63144205729165 162.0177 146.964775390625 162.0177L164.1928173828125 162.0177C167.52615071614585 162.0177 169.1928173828125 163.68436666666665 169.1928173828125 167.0177L169.1928173828125 167.0177L169.1928173828125 249.25799999999998L169.1928173828125 249.25799999999998C169.1928173828125 249.25799999999998 141.964775390625 249.25799999999998 141.964775390625 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 141.964775390625 249.25799999999998L 141.964775390625 167.0177Q 141.964775390625 162.0177 146.964775390625 162.0177L 164.1928173828125 162.0177Q 169.1928173828125 162.0177 169.1928173828125 167.0177L 169.1928173828125 167.0177L 169.1928173828125 249.25799999999998L 169.1928173828125 249.25799999999998z" pathFrom="M 141.964775390625 249.25799999999998L 141.964775390625 249.25799999999998L 169.1928173828125 249.25799999999998L 169.1928173828125 249.25799999999998L 169.1928173828125 249.25799999999998L 169.1928173828125 249.25799999999998L 169.1928173828125 249.25799999999998L 141.964775390625 249.25799999999998" cy="162.0177" cx="245.3506396484375" j="1" val="42" barHeight="87.24029999999999" barWidth="29.2280419921875"></path><path id="SvgjsPath1054" d="M246.3506396484375 249.25799999999998L246.3506396484375 98.47174999999999C246.3506396484375 95.13841666666664 248.01730631510418 93.47174999999999 251.3506396484375 93.47174999999999L268.578681640625 93.47174999999999C271.9120149739583 93.47174999999999 273.578681640625 95.13841666666664 273.578681640625 98.47174999999999L273.578681640625 98.47174999999999L273.578681640625 249.25799999999998L273.578681640625 249.25799999999998C273.578681640625 249.25799999999998 246.3506396484375 249.25799999999998 246.3506396484375 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 246.3506396484375 249.25799999999998L 246.3506396484375 98.47174999999999Q 246.3506396484375 93.47174999999999 251.3506396484375 93.47174999999999L 268.578681640625 93.47174999999999Q 273.578681640625 93.47174999999999 273.578681640625 98.47174999999999L 273.578681640625 98.47174999999999L 273.578681640625 249.25799999999998L 273.578681640625 249.25799999999998z" pathFrom="M 246.3506396484375 249.25799999999998L 246.3506396484375 249.25799999999998L 273.578681640625 249.25799999999998L 273.578681640625 249.25799999999998L 273.578681640625 249.25799999999998L 273.578681640625 249.25799999999998L 273.578681640625 249.25799999999998L 246.3506396484375 249.25799999999998" cy="93.47174999999999" cx="349.73650390625" j="2" val="75" barHeight="155.78625" barWidth="29.2280419921875"></path><path id="SvgjsPath1060" d="M350.73650390625 249.25799999999998L350.73650390625 25.771500000000003C350.73650390625 22.43816666666666 352.4031705729167 20.771500000000003 355.73650390625 20.771500000000003L372.9645458984375 20.771500000000003C376.2978792317708 20.771500000000003 377.9645458984375 22.43816666666666 377.9645458984375 25.771500000000003L377.9645458984375 25.771500000000003L377.9645458984375 249.25799999999998L377.9645458984375 249.25799999999998C377.9645458984375 249.25799999999998 350.73650390625 249.25799999999998 350.73650390625 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 350.73650390625 249.25799999999998L 350.73650390625 25.771500000000003Q 350.73650390625 20.771500000000003 355.73650390625 20.771500000000003L 372.9645458984375 20.771500000000003Q 377.9645458984375 20.771500000000003 377.9645458984375 25.771500000000003L 377.9645458984375 25.771500000000003L 377.9645458984375 249.25799999999998L 377.9645458984375 249.25799999999998z" pathFrom="M 350.73650390625 249.25799999999998L 350.73650390625 249.25799999999998L 377.9645458984375 249.25799999999998L 377.9645458984375 249.25799999999998L 377.9645458984375 249.25799999999998L 377.9645458984375 249.25799999999998L 377.9645458984375 249.25799999999998L 350.73650390625 249.25799999999998" cy="20.771500000000003" cx="454.1223681640625" j="3" val="110" barHeight="228.48649999999998" barWidth="29.2280419921875"></path><path id="SvgjsPath1066" d="M455.1223681640625 249.25799999999998L455.1223681640625 206.48354999999998C455.1223681640625 203.15021666666667 456.7890348307292 201.48354999999998 460.1223681640625 201.48354999999998L477.35041015625 201.48354999999998C480.6837434895833 201.48354999999998 482.35041015625006 203.15021666666667 482.35041015625 206.48354999999998L482.35041015625 206.48354999999998L482.35041015625 249.25799999999998L482.35041015625 249.25799999999998C482.35041015625 249.25799999999998 455.1223681640625 249.25799999999998 455.1223681640625 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 455.1223681640625 249.25799999999998L 455.1223681640625 206.48354999999998Q 455.1223681640625 201.48354999999998 460.1223681640625 201.48354999999998L 477.35041015625 201.48354999999998Q 482.35041015625 201.48354999999998 482.35041015625 206.48354999999998L 482.35041015625 206.48354999999998L 482.35041015625 249.25799999999998L 482.35041015625 249.25799999999998z" pathFrom="M 455.1223681640625 249.25799999999998L 455.1223681640625 249.25799999999998L 482.35041015625 249.25799999999998L 482.35041015625 249.25799999999998L 482.35041015625 249.25799999999998L 482.35041015625 249.25799999999998L 482.35041015625 249.25799999999998L 455.1223681640625 249.25799999999998" cy="201.48354999999998" cx="558.5082324218749" j="4" val="23" barHeight="47.774449999999995" barWidth="29.2280419921875"></path><path id="SvgjsPath1072" d="M559.5082324218749 249.25799999999998L559.5082324218749 73.54595C559.5082324218749 70.21261666666669 561.1748990885416 68.54595 564.5082324218749 68.54595L581.7362744140624 68.54595C585.0696077473958 68.54595 586.7362744140624 70.21261666666669 586.7362744140624 73.54595L586.7362744140624 73.54595L586.7362744140624 249.25799999999998L586.7362744140624 249.25799999999998C586.7362744140624 249.25799999999998 559.5082324218749 249.25799999999998 559.5082324218749 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 559.5082324218749 249.25799999999998L 559.5082324218749 73.54595Q 559.5082324218749 68.54595 564.5082324218749 68.54595L 581.7362744140624 68.54595Q 586.7362744140624 68.54595 586.7362744140624 73.54595L 586.7362744140624 73.54595L 586.7362744140624 249.25799999999998L 586.7362744140624 249.25799999999998z" pathFrom="M 559.5082324218749 249.25799999999998L 559.5082324218749 249.25799999999998L 586.7362744140624 249.25799999999998L 586.7362744140624 249.25799999999998L 586.7362744140624 249.25799999999998L 586.7362744140624 249.25799999999998L 586.7362744140624 249.25799999999998L 559.5082324218749 249.25799999999998" cy="68.54595" cx="662.8940966796874" j="5" val="87" barHeight="180.71204999999998" barWidth="29.2280419921875"></path><path id="SvgjsPath1078" d="M663.8940966796874 249.25799999999998L663.8940966796874 150.4005C663.8940966796874 147.06716666666665 665.5607633463541 145.4005 668.8940966796874 145.4005L686.122138671875 145.4005C689.4554720052083 145.4005 691.122138671875 147.06716666666665 691.122138671875 150.4005L691.122138671875 150.4005L691.122138671875 249.25799999999998L691.122138671875 249.25799999999998C691.122138671875 249.25799999999998 663.8940966796874 249.25799999999998 663.8940966796874 249.25799999999998 " fill="rgba(0,158,247,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask9es9h62y)" pathTo="M 663.8940966796874 249.25799999999998L 663.8940966796874 150.4005Q 663.8940966796874 145.4005 668.8940966796874 145.4005L 686.122138671875 145.4005Q 691.122138671875 145.4005 691.122138671875 150.4005L 691.122138671875 150.4005L 691.122138671875 249.25799999999998L 691.122138671875 249.25799999999998z" pathFrom="M 663.8940966796874 249.25799999999998L 663.8940966796874 249.25799999999998L 691.122138671875 249.25799999999998L 691.122138671875 249.25799999999998L 691.122138671875 249.25799999999998L 691.122138671875 249.25799999999998L 691.122138671875 249.25799999999998L 663.8940966796874 249.25799999999998" cy="145.4005" cx="767.2799609374999" j="6" val="50" barHeight="103.85749999999999" barWidth="29.2280419921875"></path><g id="SvgjsG1040" class="apexcharts-bar-goals-markers" style="pointer-events: none"><g id="SvgjsG1041" className="apexcharts-bar-goals-groups"></g><g id="SvgjsG1047" className="apexcharts-bar-goals-groups"></g><g id="SvgjsG1053" className="apexcharts-bar-goals-groups"></g><g id="SvgjsG1059" className="apexcharts-bar-goals-groups"></g><g id="SvgjsG1065" className="apexcharts-bar-goals-groups"></g><g id="SvgjsG1071" className="apexcharts-bar-goals-groups"></g><g id="SvgjsG1077" className="apexcharts-bar-goals-groups"></g></g></g><g id="SvgjsG1039" class="apexcharts-datalabels" data:realIndex="0"><g id="SvgjsG1044" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1046" font-family="inherit" x="51.19293212890624" y="128.0919" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="51.19293212890624" cy="128.0919" style="font-family: inherit;">54</text></g><g id="SvgjsG1050" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1052" font-family="inherit" x="155.57879638671875" y="153.0177" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="155.57879638671875" cy="153.0177" style="font-family: inherit;">42</text></g><g id="SvgjsG1056" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1058" font-family="inherit" x="259.96466064453125" y="84.47174999999999" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="259.96466064453125" cy="84.47174999999999" style="font-family: inherit;">75</text></g><g id="SvgjsG1062" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1064" font-family="inherit" x="364.35052490234375" y="11.771500000000003" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="364.35052490234375" cy="11.771500000000003" style="font-family: inherit;">110</text></g><g id="SvgjsG1068" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1070" font-family="inherit" x="468.7363891601562" y="192.48354999999998" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="468.7363891601562" cy="192.48354999999998" style="font-family: inherit;">23</text></g><g id="SvgjsG1074" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1076" font-family="inherit" x="573.1222534179686" y="59.545950000000005" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="573.1222534179686" cy="59.545950000000005" style="font-family: inherit;">87</text></g><g id="SvgjsG1080" class="apexcharts-data-labels" transform="rotate(0)"><text id="SvgjsText1082" font-family="inherit" x="677.5081176757811" y="136.4005" text-anchor="middle" dominant-baseline="auto" font-size="13px" font-weight="600" fill="#181c32" class="apexcharts-datalabel" cx="677.5081176757811" cy="136.4005" style="font-family: inherit;">50</text></g></g></g><line id="SvgjsLine1133" x1="0" y1="0" x2="730.7010498046875" y2="0" stroke="#b6b6b6" stroke-dasharray="0" stroke-width="1" stroke-linecap="butt" class="apexcharts-ycrosshairs"></line><line id="SvgjsLine1134" x1="0" y1="0" x2="730.7010498046875" y2="0" stroke-dasharray="0" stroke-width="0" stroke-linecap="butt" class="apexcharts-ycrosshairs-hidden"></line><g id="SvgjsG1135" class="apexcharts-yaxis-annotations"></g><g id="SvgjsG1136" class="apexcharts-xaxis-annotations"></g><g id="SvgjsG1137" class="apexcharts-point-annotations"></g></g><g id="SvgjsG1106" class="apexcharts-yaxis" rel="0" transform="translate(25.7989501953125, 0)"><g id="SvgjsG1107" class="apexcharts-yaxis-texts-g"><text id="SvgjsText1109" font-family="inherit" x="20" y="31.4" text-anchor="end" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-yaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1110">120H</tspan><title>120H</title></text><text id="SvgjsText1112" font-family="inherit" x="20" y="93.7145" text-anchor="end" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-yaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1113">90H</tspan><title>90H</title></text><text id="SvgjsText1115" font-family="inherit" x="20" y="156.029" text-anchor="end" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-yaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1116">60H</tspan><title>60H</title></text><text id="SvgjsText1118" font-family="inherit" x="20" y="218.34349999999998" text-anchor="end" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-yaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1119">30H</tspan><title>30H</title></text><text id="SvgjsText1121" font-family="inherit" x="20" y="280.65799999999996" text-anchor="end" dominant-baseline="auto" font-size="13px" font-weight="400" fill="#a1a5b7" class="apexcharts-text apexcharts-yaxis-label " style="font-family: inherit;"><tspan id="SvgjsTspan1122">0H</tspan><title>0H</title></text></g></g><g id="SvgjsG1027" class="apexcharts-annotations"></g></svg><div class="apexcharts-legend" style="max-height: 162.5px;"></div><div class="apexcharts-tooltip apexcharts-theme-light"><div class="apexcharts-tooltip-title" style="font-family: inherit; font-size: 12px;"></div><div class="apexcharts-tooltip-series-group" style="order: 1;"><span class="apexcharts-tooltip-marker" style="background-color: rgb(0, 158, 247);"></span><div class="apexcharts-tooltip-text" style="font-family: inherit; font-size: 12px;"><div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div><div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div><div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div></div></div></div><div class="apexcharts-yaxistooltip apexcharts-yaxistooltip-0 apexcharts-yaxistooltip-left apexcharts-theme-light"><div class="apexcharts-yaxistooltip-text"></div></div></div></div>
											<!--end::Chart-->
										</div>
										<!--end: Card Body-->
									</div>
									<!--end::Chart widget 18-->
								</div>
		

		

              </div>

     <div class="row pt-5">
            <!--begin::Col-->
            <div class="col-xl-4 mb-xl-10">
                <!--begin::Mixed Widget 6-->
                <div class="card h-xl-100">
                    <!--begin::Beader-->
                    <div class="card-header border-0 pt-3 pb-3">

                        <h2 class="line-h2 headline pb-3 pt-3">Sales Statistics</h2>

                    </div>
                    <!--end::Header-->
                    <!--begin::Body-->
                    <div class="card-body p-0 d-flex flex-column">
                        <!--begin::Stats-->
                        <div class="card-px  pb-10 flex-grow-1">
                            <!--begin::Row-->
                            <div class="row g-0 mt-5 mb-10">
                                <!--begin::Col-->
                                <div class="col">
                                    <div class="d-flex align-items-center me-2">
                                        <!--begin::Symbol-->
                                        <div class="symbol symbol-50px me-3">
                                            <div class="symbol-label bg-light-info">
                                                <!--begin::Svg Icon | path: icons/duotune/art/art007.svg-->
                                                <span class="svg-icon svg-icon-1 svg-icon-info">
                                                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <path opacity="0.3" d="M20.859 12.596L17.736 13.596L10.388 20.944C10.2915 21.0406 10.1769 21.1172 10.0508 21.1695C9.9247 21.2218 9.78953 21.2486 9.65302 21.2486C9.5165 21.2486 9.3813 21.2218 9.25519 21.1695C9.12907 21.1172 9.01449 21.0406 8.918 20.944L2.29999 14.3229C2.10543 14.1278 1.99619 13.8635 1.99619 13.588C1.99619 13.3124 2.10543 13.0481 2.29999 12.853L11.853 3.29999C11.9495 3.20341 12.0641 3.12679 12.1902 3.07452C12.3163 3.02225 12.4515 2.9953 12.588 2.9953C12.7245 2.9953 12.8597 3.02225 12.9858 3.07452C13.1119 3.12679 13.2265 3.20341 13.323 3.29999L21.199 11.176C21.3036 11.2791 21.3797 11.4075 21.4201 11.5486C21.4605 11.6898 21.4637 11.8391 21.4295 11.9819C21.3953 12.1247 21.3249 12.2562 21.2249 12.3638C21.125 12.4714 20.9989 12.5514 20.859 12.596Z" fill="currentColor"></path>
                                                        <path d="M14.8 10.184C14.7447 10.1843 14.6895 10.1796 14.635 10.1699L5.816 8.69997C5.55436 8.65634 5.32077 8.51055 5.16661 8.29469C5.01246 8.07884 4.95035 7.8106 4.99397 7.54897C5.0376 7.28733 5.18339 7.05371 5.39925 6.89955C5.6151 6.7454 5.88334 6.68332 6.14498 6.72694L14.963 8.19692C15.2112 8.23733 15.435 8.36982 15.59 8.56789C15.7449 8.76596 15.8195 9.01502 15.7989 9.26564C15.7784 9.51626 15.6642 9.75001 15.479 9.92018C15.2939 10.0904 15.0514 10.1846 14.8 10.184ZM17 18.6229C17 19.0281 17.0985 19.4272 17.287 19.7859C17.4755 20.1446 17.7484 20.4521 18.0821 20.6819C18.4158 20.9117 18.8004 21.0571 19.2027 21.1052C19.605 21.1534 20.0131 21.103 20.3916 20.9585C20.7702 20.814 21.1079 20.5797 21.3758 20.2757C21.6437 19.9716 21.8336 19.607 21.9293 19.2133C22.025 18.8195 22.0235 18.4085 21.925 18.0154C21.8266 17.6223 21.634 17.259 21.364 16.9569L19.843 15.257C19.7999 15.2085 19.7471 15.1697 19.688 15.1432C19.6289 15.1167 19.5648 15.1029 19.5 15.1029C19.4352 15.1029 19.3711 15.1167 19.312 15.1432C19.2529 15.1697 19.2001 15.2085 19.157 15.257L17.636 16.9569C17.2254 17.4146 16.9988 18.0081 17 18.6229ZM10.388 20.9409L17.736 13.5929H1.99999C1.99921 13.7291 2.02532 13.8643 2.0768 13.9904C2.12828 14.1165 2.2041 14.2311 2.29997 14.3279L8.91399 20.9409C9.01055 21.0381 9.12539 21.1152 9.25188 21.1679C9.37836 21.2205 9.51399 21.2476 9.65099 21.2476C9.78798 21.2476 9.92361 21.2205 10.0501 21.1679C10.1766 21.1152 10.2914 21.0381 10.388 20.9409Z" fill="currentColor"></path>
                                                    </svg>
                                                </span>
                                                <!--end::Svg Icon-->
                                            </div>
                                        </div>
                                        <!--end::Symbol-->
                                        <!--begin::Title-->
                                        <div>
                                            <div class="fs-4 text-dark fw-bold">$2,034</div>
                                            <div class="fs-7 text-muted fw-bold">Author Sales</div>
                                        </div>
                                        <!--end::Title-->
                                    </div>
                                </div>
                                <!--end::Col-->
                                <!--begin::Col-->
                                <div class="col">
                                    <div class="d-flex align-items-center me-2">
                                        <!--begin::Symbol-->
                                        <div class="symbol symbol-50px me-3">
                                            <div class="symbol-label bg-light-danger">
                                                <!--begin::Svg Icon | path: icons/duotune/abstract/abs027.svg-->
                                                <span class="svg-icon svg-icon-1 svg-icon-danger">
                                                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <path opacity="0.3" d="M21.25 18.525L13.05 21.825C12.35 22.125 11.65 22.125 10.95 21.825L2.75 18.525C1.75 18.125 1.75 16.725 2.75 16.325L4.04999 15.825L10.25 18.325C10.85 18.525 11.45 18.625 12.05 18.625C12.65 18.625 13.25 18.525 13.85 18.325L20.05 15.825L21.35 16.325C22.35 16.725 22.35 18.125 21.25 18.525ZM13.05 16.425L21.25 13.125C22.25 12.725 22.25 11.325 21.25 10.925L13.05 7.62502C12.35 7.32502 11.65 7.32502 10.95 7.62502L2.75 10.925C1.75 11.325 1.75 12.725 2.75 13.125L10.95 16.425C11.65 16.725 12.45 16.725 13.05 16.425Z" fill="currentColor"></path>
                                                        <path d="M11.05 11.025L2.84998 7.725C1.84998 7.325 1.84998 5.925 2.84998 5.525L11.05 2.225C11.75 1.925 12.45 1.925 13.15 2.225L21.35 5.525C22.35 5.925 22.35 7.325 21.35 7.725L13.05 11.025C12.45 11.325 11.65 11.325 11.05 11.025Z" fill="currentColor"></path>
                                                    </svg>
                                                </span>
                                                <!--end::Svg Icon-->
                                            </div>
                                        </div>
                                        <!--end::Symbol-->
                                        <!--begin::Title-->
                                        <div>
                                            <div class="fs-4 text-dark fw-bold">$706</div>
                                            <div class="fs-7 text-muted fw-bold">Commision</div>
                                        </div>
                                        <!--end::Title-->
                                    </div>
                                </div>
                                <!--end::Col-->
                            </div>
                            <!--end::Row-->
                            <!--begin::Row-->
                            <div class="row g-0">
                                <!--begin::Col-->
                                <div class="col">
                                    <div class="d-flex align-items-center me-2">
                                        <!--begin::Symbol-->
                                        <div class="symbol symbol-50px me-3">
                                            <div class="symbol-label bg-light-success">
                                                <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
                                                <span class="svg-icon svg-icon-1 svg-icon-success">
                                                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <path d="M21 10H13V11C13 11.6 12.6 12 12 12C11.4 12 11 11.6 11 11V10H3C2.4 10 2 10.4 2 11V13H22V11C22 10.4 21.6 10 21 10Z" fill="currentColor"></path>
                                                        <path opacity="0.3" d="M12 12C11.4 12 11 11.6 11 11V3C11 2.4 11.4 2 12 2C12.6 2 13 2.4 13 3V11C13 11.6 12.6 12 12 12Z" fill="currentColor"></path>
                                                        <path opacity="0.3" d="M18.1 21H5.9C5.4 21 4.9 20.6 4.8 20.1L3 13H21L19.2 20.1C19.1 20.6 18.6 21 18.1 21ZM13 18V15C13 14.4 12.6 14 12 14C11.4 14 11 14.4 11 15V18C11 18.6 11.4 19 12 19C12.6 19 13 18.6 13 18ZM17 18V15C17 14.4 16.6 14 16 14C15.4 14 15 14.4 15 15V18C15 18.6 15.4 19 16 19C16.6 19 17 18.6 17 18ZM9 18V15C9 14.4 8.6 14 8 14C7.4 14 7 14.4 7 15V18C7 18.6 7.4 19 8 19C8.6 19 9 18.6 9 18Z" fill="currentColor"></path>
                                                    </svg>
                                                </span>
                                                <!--end::Svg Icon-->
                                            </div>
                                        </div>
                                        <!--end::Symbol-->
                                        <!--begin::Title-->
                                        <div>
                                            <div class="fs-4 text-dark fw-bold">$49</div>
                                            <div class="fs-7 text-muted fw-bold">Average Bid</div>
                                        </div>
                                        <!--end::Title-->
                                    </div>
                                </div>
                                <!--end::Col-->
                                <!--begin::Col-->
                                <div class="col">
                                    <div class="d-flex align-items-center me-2">
                                        <!--begin::Symbol-->
                                        <div class="symbol symbol-50px me-3">
                                            <div class="symbol-label bg-light-primary">
                                                <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm010.svg-->
                                                <span class="svg-icon svg-icon-1 svg-icon-primary">
                                                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <path opacity="0.3" d="M3 6C2.4 6 2 5.6 2 5V3C2 2.4 2.4 2 3 2H5C5.6 2 6 2.4 6 3C6 3.6 5.6 4 5 4H4V5C4 5.6 3.6 6 3 6ZM22 5V3C22 2.4 21.6 2 21 2H19C18.4 2 18 2.4 18 3C18 3.6 18.4 4 19 4H20V5C20 5.6 20.4 6 21 6C21.6 6 22 5.6 22 5ZM6 21C6 20.4 5.6 20 5 20H4V19C4 18.4 3.6 18 3 18C2.4 18 2 18.4 2 19V21C2 21.6 2.4 22 3 22H5C5.6 22 6 21.6 6 21ZM22 21V19C22 18.4 21.6 18 21 18C20.4 18 20 18.4 20 19V20H19C18.4 20 18 20.4 18 21C18 21.6 18.4 22 19 22H21C21.6 22 22 21.6 22 21Z" fill="currentColor"></path>
                                                        <path d="M3 16C2.4 16 2 15.6 2 15V9C2 8.4 2.4 8 3 8C3.6 8 4 8.4 4 9V15C4 15.6 3.6 16 3 16ZM13 15V9C13 8.4 12.6 8 12 8C11.4 8 11 8.4 11 9V15C11 15.6 11.4 16 12 16C12.6 16 13 15.6 13 15ZM17 15V9C17 8.4 16.6 8 16 8C15.4 8 15 8.4 15 9V15C15 15.6 15.4 16 16 16C16.6 16 17 15.6 17 15ZM9 15V9C9 8.4 8.6 8 8 8H7C6.4 8 6 8.4 6 9V15C6 15.6 6.4 16 7 16H8C8.6 16 9 15.6 9 15ZM22 15V9C22 8.4 21.6 8 21 8H20C19.4 8 19 8.4 19 9V15C19 15.6 19.4 16 20 16H21C21.6 16 22 15.6 22 15Z" fill="currentColor"></path>
                                                    </svg>
                                                </span>
                                                <!--end::Svg Icon-->
                                            </div>
                                        </div>
                                        <!--end::Symbol-->
                                        <!--begin::Title-->
                                        <div>
                                            <div class="fs-4 text-dark fw-bold">$5.8M</div>
                                            <div class="fs-7 text-muted fw-bold">All Time Sales</div>
                                        </div>
                                        <!--end::Title-->
                                    </div>
                                </div>
                                <!--end::Col-->
                            </div>
                            <!--end::Row-->
                        </div>
                        <!--end::Stats-->

                    </div>
                    <!--end::Body-->
                    <div class="mixed-widget-6-chart card-rounded-bottom" data-kt-chart-color="danger" style="height: 150px; min-height: 150px;"><div id="apexchartsd8qj0d2kg" class="apexcharts-canvas apexchartsd8qj0d2kg apexcharts-theme-light" style=""><svg id="SvgjsSvg1113" width="398" height="150" xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.dev" class="apexcharts-svg" xmlns:data="ApexChartsNS" transform="translate(0, 0)" style="background: transparent;"><g id="SvgjsG1115" class="apexcharts-inner apexcharts-graphical" transform="translate(0, 0)"><defs id="SvgjsDefs1114"><clipPath id="gridRectMaskd8qj0d2kg"><rect id="SvgjsRect1118" width="405" height="153" x="-3.5" y="-1.5" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect></clipPath><clipPath id="forecastMaskd8qj0d2kg"></clipPath><clipPath id="nonForecastMaskd8qj0d2kg"></clipPath><clipPath id="gridRectMarkerMaskd8qj0d2kg"><rect id="SvgjsRect1119" width="402" height="154" x="-2" y="-2" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect></clipPath></defs><g id="SvgjsG1126" class="apexcharts-xaxis" transform="translate(0, 0)"><g id="SvgjsG1127" class="apexcharts-xaxis-texts-g" transform="translate(0, -4)"></g></g><g id="SvgjsG1135" class="apexcharts-grid"><g id="SvgjsG1136" class="apexcharts-gridlines-horizontal" style="display: none;"><line id="SvgjsLine1138" x1="0" y1="0" x2="398" y2="0" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1139" x1="0" y1="15" x2="398" y2="15" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1140" x1="0" y1="30" x2="398" y2="30" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1141" x1="0" y1="45" x2="398" y2="45" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1142" x1="0" y1="60" x2="398" y2="60" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1143" x1="0" y1="75" x2="398" y2="75" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1144" x1="0" y1="90" x2="398" y2="90" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1145" x1="0" y1="105" x2="398" y2="105" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1146" x1="0" y1="120" x2="398" y2="120" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1147" x1="0" y1="135" x2="398" y2="135" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line><line id="SvgjsLine1148" x1="0" y1="150" x2="398" y2="150" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line></g><g id="SvgjsG1137" class="apexcharts-gridlines-vertical" style="display: none;"></g><line id="SvgjsLine1150" x1="0" y1="150" x2="398" y2="150" stroke="transparent" stroke-dasharray="0" stroke-linecap="butt"></line><line id="SvgjsLine1149" x1="0" y1="1" x2="0" y2="150" stroke="transparent" stroke-dasharray="0" stroke-linecap="butt"></line></g><g id="SvgjsG1120" class="apexcharts-area-series apexcharts-plot-series"><g id="SvgjsG1121" class="apexcharts-series" seriesName="NetxProfit" data:longestSeries="true" rel="1" data:realIndex="0"><path id="SvgjsPath1124" d="M0 150L0 75C27.859999999999996 75 51.739999999999995 87.5 79.6 87.5C107.46 87.5 131.34 37.5 159.2 37.5C187.05999999999997 37.5 210.94 75 238.79999999999998 75C266.65999999999997 75 290.53999999999996 12.5 318.4 12.5C346.26 12.5 370.14 12.5 398 12.5C398 12.5 398 12.5 398 150M398 12.5C398 12.5 398 12.5 398 12.5 " fill="rgba(255,245,248,1)" fill-opacity="1" stroke-opacity="1" stroke-linecap="butt" stroke-width="0" stroke-dasharray="0" class="apexcharts-area" index="0" clip-path="url(#gridRectMaskd8qj0d2kg)" pathTo="M 0 150L 0 75C 27.859999999999996 75 51.739999999999995 87.5 79.6 87.5C 107.46 87.5 131.34 37.5 159.2 37.5C 187.05999999999997 37.5 210.94 75 238.79999999999998 75C 266.65999999999997 75 290.53999999999996 12.5 318.4 12.5C 346.26 12.5 370.14 12.5 398 12.5C 398 12.5 398 12.5 398 150M 398 12.5z" pathFrom="M -1 150L -1 150L 79.6 150L 159.2 150L 238.79999999999998 150L 318.4 150L 398 150"></path><path id="SvgjsPath1125" d="M0 75C27.859999999999996 75 51.739999999999995 87.5 79.6 87.5C107.46 87.5 131.34 37.5 159.2 37.5C187.05999999999997 37.5 210.94 75 238.79999999999998 75C266.65999999999997 75 290.53999999999996 12.5 318.4 12.5C346.26 12.5 370.14 12.5 398 12.5C398 12.5 398 12.5 398 12.5 " fill="none" fill-opacity="1" stroke="#f1416c" stroke-opacity="1" stroke-linecap="butt" stroke-width="3" stroke-dasharray="0" class="apexcharts-area" index="0" clip-path="url(#gridRectMaskd8qj0d2kg)" pathTo="M 0 75C 27.859999999999996 75 51.739999999999995 87.5 79.6 87.5C 107.46 87.5 131.34 37.5 159.2 37.5C 187.05999999999997 37.5 210.94 75 238.79999999999998 75C 266.65999999999997 75 290.53999999999996 12.5 318.4 12.5C 346.26 12.5 370.14 12.5 398 12.5" pathFrom="M -1 150L -1 150L 79.6 150L 159.2 150L 238.79999999999998 150L 318.4 150L 398 150"></path><g id="SvgjsG1122" class="apexcharts-series-markers-wrap" data:realIndex="0"><g class="apexcharts-series-markers"><circle id="SvgjsCircle1156" r="0" cx="398" cy="12.5" class="apexcharts-marker wt964nwm5 no-pointer-events" stroke="#f1416c" fill="#fff5f8" fill-opacity="1" stroke-width="3" stroke-opacity="0.9" default-marker-size="0"></circle></g></g></g><g id="SvgjsG1123" class="apexcharts-datalabels" data:realIndex="0"></g></g><line id="SvgjsLine1151" x1="0" y1="0" x2="398" y2="0" stroke="#b6b6b6" stroke-dasharray="0" stroke-width="1" stroke-linecap="butt" class="apexcharts-ycrosshairs"></line><line id="SvgjsLine1152" x1="0" y1="0" x2="398" y2="0" stroke-dasharray="0" stroke-width="0" stroke-linecap="butt" class="apexcharts-ycrosshairs-hidden"></line><g id="SvgjsG1153" class="apexcharts-yaxis-annotations"></g><g id="SvgjsG1154" class="apexcharts-xaxis-annotations"></g><g id="SvgjsG1155" class="apexcharts-point-annotations"></g></g><g id="SvgjsG1134" class="apexcharts-yaxis" rel="0" transform="translate(-18, 0)"></g><g id="SvgjsG1116" class="apexcharts-annotations"></g></svg><div class="apexcharts-legend" style="max-height: 75px;"></div><div class="apexcharts-tooltip apexcharts-theme-light" style="left: 199.781px; top: 15.5px;"><div class="apexcharts-tooltip-title" style="font-family: inherit; font-size: 12px;"></div><div class="apexcharts-tooltip-series-group apexcharts-active" style="order: 1; display: flex;"><span class="apexcharts-tooltip-marker" style="background-color: rgb(255, 245, 248);"></span><div class="apexcharts-tooltip-text" style="font-family: inherit; font-size: 12px;"><div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label">Net Profit: </span><span class="apexcharts-tooltip-text-y-value">$55 thousands</span></div><div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div><div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div></div></div></div><div class="apexcharts-xaxistooltip apexcharts-xaxistooltip-bottom apexcharts-theme-light" style="left: 379px; top: 152px;"><div class="apexcharts-xaxistooltip-text" style="font-family: inherit; font-size: 12px; min-width: 15.87px;"></div></div><div class="apexcharts-yaxistooltip apexcharts-yaxistooltip-0 apexcharts-yaxistooltip-left apexcharts-theme-light"><div class="apexcharts-yaxistooltip-text"></div></div></div></div>
                </div>

                <!--end::Mixed Widget 6-->
            </div>
            <!--end::Col-->
            <!--begin::Col-->
            <div class="col-xl-8">
                <!--begin::Tables widget 14-->
                <div class="card card-flush">
                    <!--begin::Header-->
                    <div class="card-header pt-7">
                        <!--begin::Title-->
                        <h2 class="line-h2 headline pb-3 pt-3">Top Delller List</h2>
                        <!--end::Title-->

                    </div>
                    <!--end::Header-->
                    <!--begin::Body-->
                    <div class="card-body pt-6">
                        <!--begin::Table container-->
                        <div class="table-responsive">
                            <!--begin::Table-->
                            <table class="table table-row-dashed align-middle gs-0 gy-3 my-0">
                                <!--begin::Table head-->
                                <thead>
                                    <tr class="fs-7 fw-bold text-gray-400 border-bottom-0">
                                        <th class="p-0 pb-3 min-w-175px ">Profile</th>

                                        <th class="p-0 pb-3 min-w-100px">EmailId</th>
                                        <th class="p-0 pb-3 min-w-175px  pe-12">Phone</th>
                                        <th class="p-0 pb-3 min-w-175px  pe-12">City</th>
                                        <th class="p-0 pb-3 min-w-175px  pe-12">Payment Status</th>

                                    </tr>
                                </thead>
                                <!--end::Table head-->
                                <!--begin::Table body-->
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-50px me-3">
                                                    <img src="../Dashboardimg/047-girl-25.svg" class="" alt="">
                                                </div>
                                                <div class="d-flex justify-content-start flex-column">
                                                    <a href="#" class="text-gray-800 fw-bold text-hover-primary mb-1 fs-6">Mivy App</a>
                                                    <span class="text-gray-400 fw-semibold d-block fs-7">Jane Cooper</span>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">abc@gmai.com</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">+91 999 999 9999</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">Ahmedabad</span>
                                        </td>

                                        <td class="text-end pe-12">
                                            <span class="badge py-3 px-4 fs-7 badge-light-primary">In Process</span>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-50px me-3">
                                                    <img src="../Dashboardimg/047-girl-25.svg" class="" alt="">
                                                </div>
                                                <div class="d-flex justify-content-start flex-column">
                                                    <a href="#" class="text-gray-800 fw-bold text-hover-primary mb-1 fs-6">Mivy App</a>
                                                    <span class="text-gray-400 fw-semibold d-block fs-7">Jane Cooper</span>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">abc@gmai.com</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">+91 999 999 9999</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">Ahmedabad</span>
                                        </td>

                                        <td class="text-end pe-12">
                                            <span class="badge py-3 px-4 fs-7 badge-light-warning">Panding</span>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-50px me-3">
                                                    <img src="../Dashboardimg/047-girl-25.svg" class="" alt="">
                                                </div>
                                                <div class="d-flex justify-content-start flex-column">
                                                    <a href="#" class="text-gray-800 fw-bold text-hover-primary mb-1 fs-6">Mivy App</a>
                                                    <span class="text-gray-400 fw-semibold d-block fs-7">Jane Cooper</span>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">abc@gmai.com</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">+91 999 999 9999</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">Ahmedabad</span>
                                        </td>

                                        <td class="text-end pe-12">
                                            <span class="badge py-3 px-4 fs-7 badge-light-danger">Rejected</span>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-50px me-3">
                                                    <img src="../Dashboardimg/047-girl-25.svg" class="" alt="">
                                                </div>
                                                <div class="d-flex justify-content-start flex-column">
                                                    <a href="#" class="text-gray-800 fw-bold text-hover-primary mb-1 fs-6">Mivy App</a>
                                                    <span class="text-gray-400 fw-semibold d-block fs-7">Jane Cooper</span>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">abc@gmai.com</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">+91 999 999 9999</span>
                                        </td>
                                        <td class="text-end pe-0">
                                            <span class="text-gray-600 fw-bold fs-6">Ahmedabad</span>
                                        </td>

                                        <td class="text-end pe-12">
                                            <span class="badge py-3 px-4 fs-7 badge-light-primary">Confirm</span>
                                        </td>

                                    </tr>



                                </tbody>
                                <!--end::Table body-->
                            </table>
                        </div>
                        <!--end::Table-->
                    </div>
                    <!--end: Card Body-->
                </div>
                <!--end::Tables widget 14-->
            </div>
            <!--end::Col-->
        </div>
            
                
            </div>  

            <script type="text/javascript">

                $(document).ready(function () {
                    var owl = $("#owl-demo");
                    owl.owlCarousel({
                        autoPlay: 1500,
                        items: 1, //10 items above 1000px browser width
                        itemsDesktop: [1000, 4], //5 items between 1000px and 901px
                        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
                        itemsTablet: [600, 2], //2 items between 600 and 0;
                        itemsMobile: false, // itemsMobile disabled - inherit from itemsTablet option
                        pagination: false
                    });
                    $(".next").click(function () {
                        owl.trigger('owl.next');
                    })
                    $(".prev").click(function () {
                        owl.trigger('owl.prev');
                    })
                });


                $(document).ready(function () {
                    var owl = $("#owl-demo1");
                    owl.owlCarousel({
                        autoPlay: 1500,
                        items: 1, //10 items above 1000px browser width
                        itemsDesktop: [1000, 4], //5 items between 1000px and 901px
                        itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
                        itemsTablet: [600, 2], //2 items between 600 and 0;
                        itemsMobile: false, // itemsMobile disabled - inherit from itemsTablet option
                        pagination: false
                    });
                    $(".next1").click(function () {
                        owl.trigger('owl.next');
                    })
                    $(".prev1").click(function () {
                        owl.trigger('owl.prev');
                    })

                });
            </script>
           	
		
		
	<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/d3/4.7.3/d3.min.js"></script>
    
	


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmUtility" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />
</asp:Content>


