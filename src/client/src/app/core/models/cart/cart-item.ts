export class CartItemApiModel {
    cartId: string;
    productId: string;
    quantity: number;
    productName: string;
    productDescription: string;
    rate: number;
    constructor(cartId: string, productId: string, quantity: number) {
        this.cartId = cartId;
        this.productId = productId;
        this.quantity = quantity;
    }
}