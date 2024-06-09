import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProizvodUpdate } from '../shared/models/proizvod';

@Component({
  selector: 'app-product-update-form',
  templateUrl: './product-update-form.component.html',
  styleUrls: ['./product-update-form.component.scss']
})
export class ProductUpdateFormComponent {

  constructor(
    public dialogRef: MatDialogRef<ProductUpdateFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ProizvodUpdate
  ) {}

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
