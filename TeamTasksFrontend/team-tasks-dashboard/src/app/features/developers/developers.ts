import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DeveloperService } from '../../core/services/developerService';
import { Developer } from '../../shared/models/developer.model';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-developers',
  imports: [CommonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatButtonModule],
  templateUrl: './developers.html',
  styleUrl: './developers.css',
})
export class Developers implements OnInit {

  public developers: Developer[] = [];
  public formDeveloper: FormGroup;
  public actionBtn: string = '';
  public titleForm: string = '';
  public edit: boolean = false;

  constructor(
    private _developerService: DeveloperService,
    private _modalService: NgbModal,
    private formb: FormBuilder,
  ) {
    this.formDeveloper = this.formb.group({
      developerid: [0],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      isactive: [true],
    });
  }

  ngOnInit(): void {
    this.listDeveloper();
  }

  public listDeveloper(): void {
    this._developerService.getDevelopers().subscribe({
      next: (data) => {
        this.developers = data;
      },
      error: (err) => {
        console.error('Error al obtener los desarrolladores', err);
        Swal.fire({ title: "Error", text: "Ocurrió un error al cargar los desarrolladores.", icon: "error" });
      }
    });
  }

  openModal(developerModal: any, developer: any) {
    this._modalService.open(developerModal, { centered: true, size: 'lg' });
    if (developer === 0) {
      this.edit = false;
      this.formDeveloper.reset();
      this.actionBtn = 'Crear';
      this.titleForm = 'Crear desarrollador';
    } else {
      this.edit = true;
      this.actionBtn = 'Actualizar';
      this.titleForm = 'Actualizar desarrollador';
      this.formDeveloper.patchValue({
        developerid: developer.developerid,
        firstname: developer.firstname,
        lastname: developer.lastname,
        email: developer.email,
        isactive: developer.isactive,
      });
    }
  }

  submit(): void {
    if (this.edit) {
      this.updateDeveloper();
    } else {
      this.createDeveloper();
    }
  }

  public createDeveloper(): void {
    if (this.formDeveloper.invalid) {
      this.formDeveloper.markAllAsTouched();
      return;
    }
    const body = {
      firstname: this.formDeveloper.value.firstname,
      lastname: this.formDeveloper.value.lastname,
      email: this.formDeveloper.value.email,
      isactive: this.formDeveloper.value.isactive,
    };
    this._developerService.createDeveloper(body).subscribe({
      next: () => {
        Swal.fire('Creado', 'Desarrollador creado correctamente', 'success');
        this._modalService.dismissAll();
        this.listDeveloper();
      },
      error: (err) => {
        Swal.fire('Error', err.error.result, 'error');
      }
    });
  }

  public updateDeveloper(): void {
    if (this.formDeveloper.invalid) {
      this.formDeveloper.markAllAsTouched();
      return;
    }
    const body = this.formDeveloper.value;
    this._developerService.updateDeveloper(body).subscribe({
      next: () => {
        Swal.fire('Actualizado', 'Desarrollador actualizado correctamente', 'success');
        this._modalService.dismissAll();
        this.listDeveloper();
      },
      error: (err) => {
        Swal.fire('Error', err.error.result, 'error');
      }
    });
  }

  public deleteDeveloper(developer: Developer): void {
    Swal.fire({
      title: '¿Eliminar desarrollador?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
    }).then((result) => {
      if (result.isConfirmed) {
        this._developerService.deleteDeveloper(developer.developerid).subscribe({
          next: () => {
            Swal.fire('Eliminado', 'Desarrollador eliminado correctamente', 'success');
            this.listDeveloper();
          },
          error: (err) => {
            Swal.fire('Error', err.error.result, 'error');
          }
        });
      }
    });
  }
}
