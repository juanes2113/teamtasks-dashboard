import { CreateDeveloperDto, Developer, UpdateDeveloperDto } from '../../shared/models/developer.model';
import { ApiResponse } from '../../shared/models/response.model';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DeveloperService {

  constructor(private readonly http: HttpClient) { }

  public getDevelopers(): Observable<Developer[]> {
    return this.http.get<ApiResponse<Developer[]>>(`${environment.apiUrl}/Developer`).pipe(
      map(response => response.result)
    );
  }

  public getByIdDeveloper(id: number): Observable<Developer> {
    return this.http.get<ApiResponse<Developer>>(`${environment.apiUrl}/Developer/${id}`).pipe(
      map(response => response.result)
    );
  }

  public createDeveloper(body: CreateDeveloperDto): Observable<boolean> {
    return this.http.post<ApiResponse<boolean>>(`${environment.apiUrl}/Developer`, body).pipe(
      map(response => response.result)
    );
  }

  public updateDeveloper(body: UpdateDeveloperDto): Observable<boolean> {
    return this.http.put<ApiResponse<boolean>>(`${environment.apiUrl}/Developer`, body).pipe(
      map(response => response.result)
    );
  }

  public deleteDeveloper(id: number): Observable<boolean> {
    return this.http.delete<ApiResponse<boolean>>(`${environment.apiUrl}/Developer/${id}`).pipe(
      map(response => response.result)
    );
  }
}
