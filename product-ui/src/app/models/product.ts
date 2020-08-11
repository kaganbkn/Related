import { Tag } from './tag';

export class Product {
    productId: string;
    name: string;
    tags: Tag[];
    price: number;
    isDeleted: boolean;
}
