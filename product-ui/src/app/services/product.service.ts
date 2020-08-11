import { Tag } from './../models/tag';
import { Product } from './../models/product';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { AlertifyService } from './alertify.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient,private alertifyService: AlertifyService, private router: Router) { }
  path = 'https://localhost:44344/api/product/';

  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.path );
  }
  getProductsById(productId): Observable<Product> {
    return this.httpClient.get<Product>(this.path + productId);
  }
  deleteProductsById(productId): Observable<Product> {
    this.alertifyService.warning('product deleted');
    return this.httpClient.delete<Product>(this.path + productId);
  }
  deleteTagById(productId, tagId): Observable<Tag> {
    this.alertifyService.warning('tag deleted');
    return this.httpClient.delete<Tag>(this.path + productId + '/tag/' + tagId);
  }
  add(product){
    this.httpClient.post(this.path, product).subscribe(data=>{
      this.router.navigateByUrl('/productDetail/' + data['productId']);
    });
  }
}
