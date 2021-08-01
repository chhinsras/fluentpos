export interface Product {
  id: string;
  name: string;
  localeName: string;
  brandId: string;
  brandName: string;
  categoryId: string;
  categoryName: string;
  price: number;
  cost: number;
  imageUrl: string;
  tax: string;
  taxMethod: string;
  barcodeSymbology: string;
  isAlert?: boolean;
  alertQuantity: number;
  detail: string;
}
