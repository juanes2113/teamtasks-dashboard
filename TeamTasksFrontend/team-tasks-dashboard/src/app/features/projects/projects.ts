import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProjectService } from '../../core/services/projectService';
import { ProjectSummary } from '../../shared/models/project.model';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-projects',
  imports: [CommonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatButtonModule],
  templateUrl: './projects.html',
  styleUrl: './projects.css',
})
export class Projects implements OnInit {

  public projects: ProjectSummary[] = [];
  public formProject: FormGroup;
  public actionBtn: string = '';
  public titleForm: string = '';
  public edit: boolean = false;

  constructor(
    private readonly _projectService: ProjectService,
    private _modalService: NgbModal,
    private formb: FormBuilder,
    private router: Router
  ) {
    this.formProject = this.formb.group({
      name: ['', Validators.required],
      clientname: ['', Validators.required],
      startdate: ['', [Validators.required]],
      enddate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.listProject();
  }

  public listProject(): void {
    this._projectService.getProjects().subscribe({
      next: (data) => {
        this.projects = data;
      },
      error: (err) => {
        console.error('Error al obtener los proyectos', err);
        Swal.fire({ title: "Error", text: "Ocurrió un error al cargar los proyectos.", icon: "error" });
      }
    });
  }

  public goToTasks(projectId: number): void {
    this.router.navigate(['/projects', projectId, 'tasks']);
  }

  openModal(projectModal: any, project: any) {
    this._modalService.open(projectModal, { centered: true, size: 'lg' });
    if (project === 0) {
      this.edit = false;
      this.formProject.reset();
      this.actionBtn = 'Crear';
      this.titleForm = 'Crear proyecto';
    } else {
    }
  }

  submit(): void {
    if (this.edit) {
    } else {
      this.createProject();
    }
  }

  public createProject(): void {
    if (this.formProject.invalid) {
      this.formProject.markAllAsTouched();
      return;
    }
    const body = {
      name: this.formProject.value.name,
      clientname: this.formProject.value.clientname,
      startdate: this.formProject.value.startdate,
      enddate: this.formProject.value.enddate,
    };
    this._projectService.createProject(body).subscribe({
      next: () => {
        Swal.fire('Creado', 'Proyecto creado correctamente', 'success');
        this._modalService.dismissAll();
        this.listProject();
      },
      error: (err) => {
        Swal.fire('Error', err.error.result, 'error');
      }
    });
  }

  public deleteProject(project: ProjectSummary): void {
    Swal.fire({
      title: '¿Eliminar proyecto?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
    }).then((result) => {
      if (result.isConfirmed) {
        this._projectService.deleteProject(project.projectId).subscribe({
          next: () => {
            Swal.fire('Eliminado', 'Proyecto eliminado correctamente', 'success');
            this.listProject();
          },
          error: (err) => {
            Swal.fire('Error', err.error.result, 'error');
          }
        });
      }
    });
  }
}
