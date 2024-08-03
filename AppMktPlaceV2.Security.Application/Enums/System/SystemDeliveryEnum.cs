namespace AppMktPlaceV2.Application.Enums
{
    public static class SystemDeliveryEnum
    {
        public static Guid HandDelivery => Guid.Parse("f5c91ff9-075d-4723-baac-a1cb8e7e41b2");
        public static Guid DeliveryFromShopApp => Guid.Parse("86c9efc9-9812-442d-a8b5-8fed62a3f35c");
        public static Guid ThirdPartyDelivery => Guid.Parse("48d51e3a-6f27-4916-94b9-e9ad53c9e8bb");
    }
}
