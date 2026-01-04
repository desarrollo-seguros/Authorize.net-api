using System.ComponentModel;

namespace Authorize.NET_API
{
	public enum AuthorizeResultCode
	{
		Ok,
		Error,
		Unknown
	}

	#region Enums Transaction
	public enum TransactionTypeEnum
	{
		[Description("Authorization w/ Auto Capture")]
		authCaptureTransaction,

		[Description("Authorization Only")]
		authOnlyTransaction,

		[Description("Capture Only")]
		captureOnlyTransaction,

		[Description("Void")]
		refundTransaction,

		[Description("Unknown")]
		Unknown
	}

	public enum TransactionStatusEnum
	{
		[Description("Authorized/Pending Capture")]
		authorizedPendingCapture,

		[Description("Captured/Pending Settlement")]
		capturedPendingSettlement,

		[Description("Communication Error")]
		communicationError,

		[Description("Refund Settled Successfully")]
		refundSettledSuccessfully,

		[Description("Refund/Pending Settlement")]
		refundPendingSettlement,

		[Description("Approved Review")]
		approvedReview,

		[Description("Declined")]
		declined,

		[Description("Could Not Void")]
		couldNotVoid,

		[Description("Expired")]
		expired,

		[Description("General Error")]
		generalError,

		[Description("Failed Review")]
		failedReview,

		[Description("Settled Successfully")]
		settledSuccessfully,

		[Description("Settled Error")]
		settlementError,

		[Description("Under Review")]
		underReview,

		[Description("Voided")]
		voided,

		[Description("FDS Pending Review")]
		FDSPendingReview,

		[Description("FDS Authorized/Pending Review")]
		FDSAuthorizedPendingReview,

		[Description("Returned Item")]
		returnedItem,

		[Description("Unknown")]
		Unknown
	}
	#endregion
}