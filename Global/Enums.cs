namespace StansAssociates_Backend.Global
{
    public enum OTPLength
    {
        Small = 4,
        Standard = 6,
        Big = 8
    }


    public enum MediaType
    {
        ProfileImage,
        TeamProfile,
        VendorProcurementMedia,
        Vendor
    }


    public enum OTPType
    {
        Register = 1,
        Login,
        UpdateProfile,
        EmailChange,
        MobileNumberChange
    }


    public enum PaymentStatus
    {
        Initialized = 1,
        Processing,
        Success,
        Pending,
        Failed,
        RefundInitiated,
        RefundFailed,
        RefundSuccess
    }


    public enum OrderStatus
    {
        Initiated = 1,
        Success = 2,
        Cancelled = 3,
        PaymentPending = 4,
        PaymentFailed = 5
    }


    public enum VerificationStatus
    {
        Verified,
        NotVerified
    }


    public enum PaymentMethod
    {
        card = 1,
        netbanking = 2,
        upi = 3,
        paylater = 4,
        wallet = 5,
        cardless_emi = 6,
        other = 7,
        userwallet = 8,
        subscription = 9,
        cash = 10
    }


    public enum WalletTransactionType
    {
        Credit = 1,
        Debit,

    }


    public enum Source
    {
        Subscription = 1,
        Order,
        Recharge,
        Refund,
        StorePurchase
    }


    public enum SubscriptionType
    {
        Daily = 1,
        Interval,
        Custom
    }


    public enum DayOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    //public enum UnitType
    //{
    //    Piece = 1,
    //    Gram,
    //    Milliliter,
    //}

    public enum StoreTransactionStatus
    {
        Created = 1,
        //Approved,
        Packed,
        ReadyToDispatch,
        Dispatched,
        InTransit,
        Received,
        ProductsVerified,
        Cancelled
    }


    public enum DeliveryStatus
    {
        PendingAcceptance = 1, // after order placed
        Assigned,           // after a delivery boy accepts
        Dispatched,
        InTransit,
        OTPVerification,
        Delivered,
        Failed,
        Cancelled,
        Returned
    }


    public enum TransferType
    {
        ByHand = 1,
        ByVehicle
    }


    public enum ProductSortBy
    {
        New = 1,
        PriceAsc,
        PriceDesc,
        BetterDiscount
    }


    public static class CommonClass
    {
        public static int GetPaymentMethod(string PaymentMathod)
        {

            return PaymentMathod switch
            {
                "card" => (int)PaymentMethod.card,
                "netbanking" => (int)PaymentMethod.netbanking,
                "upi" => (int)PaymentMethod.upi,
                "paylater" => (int)PaymentMethod.paylater,
                "wallet" => (int)PaymentMethod.wallet,
                "cardless_emi" => (int)PaymentMethod.cardless_emi,
                _ => (int)PaymentMethod.other,
            };
        }

        public enum TopicTypes
        {
            Advertisement = 1,
            Product,
            Category
        }
    }
}
