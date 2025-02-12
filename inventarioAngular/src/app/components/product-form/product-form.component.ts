import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { HttpService } from '../../http.service';
import { IProduct } from '../../interfaces/product';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-form',
  imports: [MatInputModule, MatButtonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css'
})
export class ProductFormComponent {
  httpService = inject(HttpService);
  formBuilder = inject(FormBuilder);
  router = inject(Router);
  route = inject(ActivatedRoute);
  toastr = inject(ToastrService);
  productForm = this.formBuilder.group({
    nombre: ['', [Validators.required]],
    stock: [0, [Validators.required]],
    fechaCreacion: [new Date(), [Validators.required]]
  });
  productId!: number;
  isEdit = false;
  ngOnInit() {
    this.productId = this.route.snapshot.params['id'];
    if (this.productId) {
      this.isEdit = true;
      this.httpService.getProduct(this.productId).subscribe(result => {
        console.log(result);
        this.productForm.patchValue(result);
        //this.productForm.controls.fechaCreacion.disable();
      })
    }
  }
  save() {
    console.log(this.productForm.value);
    const product: IProduct = {
      id: this.productId!,
      nombre: this.productForm.value.nombre!,
      stock: this.productForm.value.stock!,
      fechaCreacion: this.productForm.value.fechaCreacion!
    };
    if (this.isEdit) {
      this.httpService.updateProduct(this.productId, product).subscribe(() => {
        console.log("Success");
        this.toastr.success('Producto actualizado correctamente');
        this.router.navigateByUrl('/product-list');
      });
    } else {
      this.httpService.createProduct(product).subscribe(() => {
        console.log("Success");
        this.toastr.success('Producto creado correctamente');
        this.router.navigateByUrl('/product-list');
      });
    }
  }
}
