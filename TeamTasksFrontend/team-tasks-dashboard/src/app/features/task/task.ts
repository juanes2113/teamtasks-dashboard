import { TaskDto, TaskPriority, TasksStatus } from '../../shared/models/task.model';
import { ProjectService } from '../../core/services/projectService';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-task',
  imports: [CommonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatButtonModule],
  templateUrl: './task.html',
  styleUrl: './task.css',
})
export class Task implements OnInit {

  public tasks: TaskDto[] = [];
  public projectId!: number;
  public formTask: FormGroup;
  public actionBtn: string = '';
  public titleForm: string = '';
  public edit: boolean = false;
  public TasksStatus = TasksStatus;
  public TaskPriority = TaskPriority;

  constructor(
    private formb: FormBuilder,
    private route: ActivatedRoute,
    private _modalService: NgbModal,
    private _taskService: ProjectService
  ) {
    this.formTask = this.formb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: [TasksStatus.ToDo, Validators.required],
      priority: [TaskPriority.Medium, Validators.required],
      estimatedcomplexity: [1, Validators.required],
      duedate: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadTasks();
  }

  private loadTasks(): void {
    this._taskService.getTasksByProject(this.projectId).subscribe({
      next: (data) => {
        this.tasks = data.map(task => ({
          ...task,
          status: task.status as TasksStatus,
          priority: task.priority as TaskPriority
        }));
      },
      error: () =>
        Swal.fire('Error', 'No se pudieron cargar las tareas', 'error')
    });
  }

  getStatusLabel(status: TasksStatus): string {
    return TasksStatus[status];
  }

  getPriorityLabel(priority: TaskPriority): string {
    return TaskPriority[priority];
  }

  openModal(taskModal: any, index: number): void {
    this.edit = false;
    this.formTask.reset({
      status: TasksStatus.ToDo,
      priority: TaskPriority.Medium,
      estimatedcomplexity: 1
    });
    this.actionBtn = 'Crear';
    this.titleForm = 'Crear tarea';
    this._modalService.open(taskModal, { centered: true, size: 'lg' });
  }

  submit(): void {
    if (this.formTask.invalid) {
      this.formTask.markAllAsTouched();
      return;
    }
    this.createTask();
  }

  private createTask(): void {
    const body = {
      projectid: this.projectId,
      title: this.formTask.value.title,
      description: this.formTask.value.description,
      assigneeid: 1,
      status: this.formTask.value.status,
      priority: this.formTask.value.priority,
      estimatedcomplexity: this.formTask.value.estimatedcomplexity,
      duedate: this.formTask.value.duedate
    };
    this._taskService.createTask(body).subscribe({
      next: () => {
        Swal.fire('Creado', 'Tarea creada correctamente', 'success');
        this._modalService.dismissAll();
        this.loadTasks();
      },
      error: () => {
        Swal.fire('Error', 'No se pudo crear la tarea', 'error');
      }
    });
  }
}
