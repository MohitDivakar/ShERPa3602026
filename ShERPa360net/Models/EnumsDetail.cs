public enum PRODUCTSTATUS
{
    PENDING         = 11227,
    TESTED          = 11228,
    APPROVED        = 11235,
    LISTED          = 11229,
    UNLISTED        = 11238,
    ORDERRECEIVED   = 11301,
    REJECTED        = 11233,
    PURCHASE        = 11398,
    NOTAVAILABLE    = 11894,
    RETURNED        = 11920,
    RESERVED        = 11925,
    LISTEDRESERVED  = 100,  
    RETURNREQUESTGENERATED = 11999,
    RETURNHANDOVERTOBDO = 12000
}

public enum PLATFORM
{
    Amazon      = 1,
    Flipkart    = 2,
    Website     = 3
}




public enum REPAIRACTION
{
    IR = 340,
    BER = 341
}

public enum REPAIRSTATUS
{
    NOTIFICATION    = 13,
    PRESCANNING     = 16,
    REPAIRED        = 63,
    IR              = 64,
    BER             = 65,
    FAILED          = 342,
    DISPATCH        = 66,
}

enum JOBSTATUS : int
{
    ForwDocGen = 22,        
}

enum SMTYPE : int
{
    ASM = 11401,
}

public enum enumJobStatus
{
    //General
    Canceled = 3,
    Saved = 4,
    //Job sheet related
    DocGenererated = 5,
    DocEmailed = 6,
    DocRcvd = 7,
    DocVerified = 8,
    WaitForPackingConf = 9,
    ReadyForPickup = 10,
    RevWayBillGen = 11,
    RevDocGen = 12,
    WaitForPickup = 13,
    ProdReceived = 14,
    ProdVerified = 15,
    AtAsc = 60,
    RcvdFromAsc = 61,
    JobCardCreated = 16,
    JobCardPrinted = 19,
    UnderProd = 20,
    ForwWayBillGen = 21,
    ForwDocGen = 22,
    Closed = 23,
    Dispatched = 26,
    DispatchEmailSent = 55,
    EscForDoc = 28,
    Esc2ForDoc = 29,
    EscForPack = 30,
    Esc2ForPack = 31,
    Estimated = 33,
    WaitForApproval = 34,
    Approved = 35,
    PartApproved = 37,
    PhyDocsVerify = 53,
    ProdPickedUp = 54,
    PendingRcpt = 62,
    RevDocsSent = 63,
    //Job Card Related
    JCSaved = 17,
    JCInProd = 52,
    JCFectoryReset = 39,
    JCSoftWareReset = 42,
    JCL1 = 43,
    JCL2 = 44,
    JCWaitingforParts = 45,
    JCL2_AfterPart = 46,
    JCL3 = 47,
    JCL4 = 48,
    JCQC1 = 49,
    JCSoaking = 50,
    JCReadyForDispatch = 40,
    JCClosed = 41,
    JCOnHold = 38,
    JCInwardInsp = 56
}