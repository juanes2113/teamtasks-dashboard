import { DeveloperRisk, DeveloperWorkload } from '../../shared/models/developer.model';
import { ApiResponse } from '../../shared/models/response.model';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {

  constructor(private readonly http: HttpClient) { }

  public getDeveloperWorkload(developerId?: number): Observable<DeveloperWorkload[]> {
    const url = developerId
      ? `${environment.apiUrl}/Dashboard/workload?developerId=${developerId}`
      : `${environment.apiUrl}/Dashboard/workload`;
    return this.http.get<ApiResponse<DeveloperWorkload[]>>(url)
      .pipe(map(response => response.result));
  }

  public getDeveloperRisk(developerId?: number): Observable<DeveloperRisk[]> {
    const url = developerId
      ? `${environment.apiUrl}/Dashboard/risk?developerId=${developerId}`
      : `${environment.apiUrl}/Dashboard/risk`;
    return this.http
      .get<ApiResponse<DeveloperRisk[]>>(url)
      .pipe(map(response => response.result));
  }
}
