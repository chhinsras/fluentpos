export interface Product {
  id: string;
  name: string;
  localeName: string;
  brandName: string;
  categoryName: string;
  price: number;
  cost: number;
  imageUrl: string;
  tax: number;
  taxMethod: string;
  barcodeSymbology: string;
  isAlert?: boolean;
  alertQuantity: number;
  detail: string;
}
