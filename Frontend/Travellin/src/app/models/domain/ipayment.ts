export interface IPayment {
  id: number;
  type: 'Payout' | 'Payment';
  user: string;
  amount: number;
  status: 'Pending' | 'Completed' | 'Failed';
  date: string;
  transactionId: string;
}