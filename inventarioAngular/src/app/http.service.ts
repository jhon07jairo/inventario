import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { IProduct } from './interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  apiUrl = "http://localhost:5113"
  http = inject(HttpClient);
  constructor() { }

  getAllProducts() { 
    return this.http.get<IProduct[]>(this.apiUrl + "/api/inventario/productos")
  }

  createProduct(product: IProduct) {
    return this.http.post(this.apiUrl + "/api/inventario/productos", product)
  }

  getProduct(productId: number) { 
    return this.http.get<IProduct>(this.apiUrl + "/api/inventario/productos/" + productId)
  }

  updateProduct(productId : number, product: IProduct) {
    return this.http.put<IProduct>(this.apiUrl + "/api/inventario/productos/" + productId, product)
  }

  deleteProduct(productId: number) {
    return this.http.delete(this.apiUrl + "/api/inventario/productos/" + productId)
  }
}
