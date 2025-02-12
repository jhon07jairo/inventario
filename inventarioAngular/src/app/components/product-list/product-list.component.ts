import { Component, inject } from '@angular/core';
import { IProduct } from '../../interfaces/product';
import { HttpService } from '../../http.service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule, RouterLink],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  router = inject(Router);
  productList: IProduct[] = [];
  httpService = inject(HttpService);
  toaster = inject(ToastrService);
  displayedColumns: string[] = ['id', 'nombre', 'stock', 'fechaCreacion', 'action'];
  ngOnInit() {
    this.getProductFromServer();
  }
  getProductFromServer(){
    this.httpService.getAllProducts().subscribe((result) => {
      this.productList = result;
      console.log(this.productList);
    });
  }
  edit(id:number) {
    console.log("Edit: " + id);
    this.router.navigateByUrl('/product/' + id);
  }
  delete(id:number) {
    this.httpService.deleteProduct(id).subscribe(() => {
      console.log("Deleted: " + id);
      this.toaster.success('Producto eliminado');
      //this.productList = this.productList.filter(product => product.id !== id);
      this.getProductFromServer();
    });
  }
}
