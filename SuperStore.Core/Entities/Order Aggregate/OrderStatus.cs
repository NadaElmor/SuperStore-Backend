using System.Runtime.Serialization;

namespace SuperStore.Core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Recieved")]
        PaymentRecieved,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}