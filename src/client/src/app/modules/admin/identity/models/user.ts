export interface User {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    emailConfirmed: boolean;
    curentPassword: string;
    password: string;
    confirmPassword: string;
    isActive: boolean;
    phoneNumber: string;
    phoneNumberConfirmed: boolean;
    profilePictureUrl: string;
}
