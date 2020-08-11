import { Tag } from './../models/tag';
import { ProductService } from './../services/product.service';
import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [ProductService]
})
export class ProductComponent implements OnInit {

  constructor(private productService: ProductService) { }
  products: Product[];
  ngOnInit() {
    this.get();
  }
  get(){
    this.productService.getProducts().subscribe(data => {
      console.log(data);
      this.products = data;
    });
  }
  delete(productId: string, name: string){
    if(confirm('Are you sure to delete ' + name + ' product')) {
      this.productService.deleteProductsById(productId).subscribe(data => {
        this.get();
      });
    }
  }
  deleteTag(productId: string, tagId: string, name: string){
    if(confirm('Are you sure to delete ' + name + ' tag')) {
      this.productService.deleteTagById(productId, tagId).subscribe(data => {
        this.get();
      });
    }
  }
}
