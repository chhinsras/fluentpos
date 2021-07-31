export class CartItem {
    id: string;
    productId: string;
    quantity: number;
    displayName: string;
    category: string;
    rate: number;
    total: number;
    constructor(productId: string, quantity: number, displayName: string, category: string, rate: number) {
        this.productId = productId;
        this.quantity = quantity;
        this.displayName = displayName;
        this.category = category;
        this.rate = rate;
    }
}