import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductFormComponent } from './components/product-form/product-form.component';

export const routes: Routes = [
    {
        path:"",
        component: ProductListComponent,
    },
    {
        path: "product-list",
        component: ProductListComponent,
    },
    {
        path: "create-product",
        component: ProductFormComponent,
    },
    {
        path: "product/:id",
        component: ProductFormComponent,
    },
];
