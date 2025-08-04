
    // export interface TransferRequestDto
    // {
    //     public string PaymentIntentId { get; set; }
    //     public string HostStripeAccountId { get; set; }
    //     public long AmountInCents { get; set; }
    // }

export interface TransferRequest
{
    paymentIntentId: string;
    hostStripeAccountId: string;
    amountInCents: string;
}