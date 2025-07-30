import { Component } from '@angular/core';
import { StripeTransferService, TransferRequest } from '../../services/TransferRequest.service';

@Component({
  selector: 'app-check-in',
  templateUrl: './check-in.html',
})
export class CheckInComponent {

  constructor(private transferService: StripeTransferService) {}

  onCheckIn() {
    const transferData: TransferRequest = {
      paymentIntentId: 'pi_XXXXXXX',          // 🎯 من الـ booking أو payment response
      hostStripeAccountId: 'acct_XXXXXXX',     // 🎯 حساب الـ host على Stripe
      amountInCents: 5000                      // 🎯 مثلًا 50.00 دولار
    };

    this.transferService.transferToHost(transferData).subscribe({
      next: (res) => {
        console.log('✅ Transfer successful:', res);
        alert('✅ الفلوس اتحولت للـ host');
      },
      error: (err) => {
        console.error('❌ Transfer failed:', err);
        alert('❌ فشل تحويل الفلوس للـ host');
      }
    });
  }
}