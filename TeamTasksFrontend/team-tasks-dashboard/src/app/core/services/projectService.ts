import { CreateProjectDto, ProjectSummary } from '../../shared/models/project.model';
import { ApiResponse } from '../../shared/models/response.model';
import { environment } from '../../../environments/environment';
import { CreateTaskDto, TaskDto } from '../../shared/models/task.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {

  constructor(private readonly http: HttpClient) { }

  public getProjects(): Observable<ProjectSummary[]> {
    return this.http.get<ApiResponse<ProjectSummary[]>>(`${environment.apiUrl}/Project`).pipe(
      map(response => response.result)
    );
  }

  public createProject(body: CreateProjectDto): Observable<boolean> {
    return this.http
      .post<ApiResponse<boolean>>(`${environment.apiUrl}/Project`, body)
      .pipe(map(response => response.result));
  }

  public deleteProject(id: number): Observable<boolean> {
    return this.http.delete<ApiResponse<boolean>>(`${environment.apiUrl}/Project/${id}`).pipe(
      map(response => response.result)
    );
  }

  public getTasksByProject(projectId: number): Observable<TaskDto[]> {
    return this.http.post<ApiResponse<TaskDto[]>>(`${environment.apiUrl}/Project/tasks`, { projectid: projectId }).pipe(
      map(response => response.result)
    );
  }

  public createTask(body: CreateTaskDto): Observable<boolean> {
    return this.http.post<ApiResponse<boolean>>(`${environment.apiUrl}/ProjectTask`, body)
      .pipe(map(response => response.result));
  }
}
