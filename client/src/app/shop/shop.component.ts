import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm?: ElementRef;
  products?: IProduct[];
  brands?: IBrand[];
  types?: IType[];
  shopParams = new ShopParams();
  totalCount: number = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams)
      .subscribe(response => {
        this.products = response?.data;
        this.shopParams.pageNumber = response?.pageIndex ?? 0; //1
        this.shopParams.pageSize = response?.pageSize ?? 0; //6
        this.totalCount = response?.count ?? 0;
      }, error => {
        console.log(error);
      });
  }

  getBrands() {
    this.shopService.getBrands()
      .subscribe(response => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
      }, error => {
        console.log(error);
      });
  }

  getTypes() {
    this.shopService.getTypes()
      .subscribe(response => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      }, error => {
        console.log(error);
      });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onPageChanged(pageNumber: number) {
    if (this.shopParams.pageNumber != pageNumber) {
      this.shopParams.pageNumber = pageNumber;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.getProducts();
  }

  onReset() {
    if (this.searchTerm) {
      this.searchTerm.nativeElement.value = '';
    }
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}
