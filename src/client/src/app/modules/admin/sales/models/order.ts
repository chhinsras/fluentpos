export interface Order {
  id: string;
  referenceNumber: string;
  timeStamp: Date;
  customerId: string;
  customerName: string;
  customerPhone: string;
  customerEmail: string;
  subTotal: number;
  tax: number;
  discount: number;
  total: number;
  isPaid: boolean;
  note: string;
}
