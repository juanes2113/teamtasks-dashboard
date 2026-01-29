import { Component, OnInit } from '@angular/core';
import { DeveloperRisk, DeveloperWorkload } from '../../shared/models/developer.model';
import { Chart } from 'chart.js/auto';
import { DashboardService } from '../../core/services/dashboardService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {

  public workloads: DeveloperWorkload[] = [];
  public developers: DeveloperRisk[] = [];
  public chart!: Chart;

  constructor(private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loadWorkload();
    this.listDeveloper();
  }

  private loadWorkload(): void {
    this._dashboardService.getDeveloperWorkload().subscribe({
      next: (data) => {
        this.workloads = data;
        this.createChart();
      },
      error: (err) => {
        console.error('Error cargando workload', err);
      }
    });
  }

  private createChart(): void {
    const labels = this.workloads.map(w => w.developerName);
    const openTasks = this.workloads.map(w => w.openTasksCount);
    const avgComplexity = this.workloads.map(w => w.averageEstimatedComplexity);
    this.chart = new Chart('workloadChart', {
      type: 'bar',
      data: {
        labels,
        datasets: [
          {
            label: 'Tareas abiertas',
            data: openTasks,
            yAxisID: 'y',
          },
          {
            label: 'Complejidad promedio',
            data: avgComplexity,
            type: 'line',
            yAxisID: 'y1',
            tension: 0.3,
            pointRadius: 5,
            fill: false
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            position: 'top'
          }
        },
        scales: {
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Tareas abiertas'
            }
          },
          y1: {
            beginAtZero: true,
            position: 'right',
            grid: {
              drawOnChartArea: false
            },
            title: {
              display: true,
              text: 'Complejidad promedio'
            }
          }
        }
      }
    });
  }

  public listDeveloper(): void {
    this._dashboardService.getDeveloperRisk().subscribe({
      next: (data) => {
        this.developers = data;
      },
      error: (err) => {
        console.error('Error al obtener los desarrolladores', err);
        Swal.fire({ title: "Error", text: "Ocurrió un error al cargar los desarrolladores.", icon: "error" });
      }
    });
  }
}
