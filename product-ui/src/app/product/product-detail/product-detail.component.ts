import { ProductService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product';


@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  providers: [ProductService]
})
export class ProductDetailComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private productService: ProductService) { }
  product: Product;
  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getProductById(params.productId);
    });
  }

  getProductById(productId){
    this.productService.getProductsById(productId).subscribe(data => {
      this.product = data;
    });

  }

}
