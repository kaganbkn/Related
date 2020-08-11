import { AlertifyService } from './../../services/alertify.service';
import { ProductService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Product } from 'src/app/models/product';
import { Tag } from 'src/app/models/tag';
import { renderFlagCheckIfStmt } from '@angular/compiler/src/render3/view/template';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.css'],
  providers: [ProductService]
})
export class ProductAddComponent implements OnInit {

  constructor(private productService: ProductService, private formBuilder: FormBuilder, private alertifyService: AlertifyService) { }

  product: Product;
  productAddForm: FormGroup;
  values: string[];
  createProductForm() {
    this.productAddForm = this.formBuilder.group({
      name: ['', Validators.required],
      price: ['', Validators.required],
      //tags: ['', Validators.required]

    });
  }

  ngOnInit() {
    this.createProductForm();
  }

  add() {
    if (this.productAddForm.valid) {
      this.product = Object.assign({}, this.productAddForm.value);
/*
      this.values = this.productAddForm.value.tags.split(',');
      this.values.forEach(c => {
        let tempTag = new Tag();
        tempTag.value = c;
        this.product.tags.push(tempTag);
      });
*/
      this.productService.add(this.product);
      this.alertifyService.success('product added.');
    }
  }

}

