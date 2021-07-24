export class Cart {
    productId: string;
    quantity: number;
    displayName: string;
    category:string;
    constructor(productId: string, quantity: number, displayName: string,category:string) {
        this.productId = productId;
        this.quantity = quantity;
        this.displayName = displayName;
        this.category = category;
    }
}