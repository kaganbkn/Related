import { ProductAddComponent } from './product/product-add/product-add.component';
import { ProductDetailComponent } from './product/product-detail/product-detail.component';
import { ProductComponent } from './product/product.component';
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {path : 'product', component: ProductComponent},
  {path : 'productAdd', component: ProductAddComponent},
  {path : 'productDetail/:productId', component: ProductDetailComponent},
  {path : '**', redirectTo: 'product' , pathMatch : 'full'}
]
