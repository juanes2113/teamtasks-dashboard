import { TestBed } from '@angular/core/testing';

import { Developers } from './developers';

describe('Developers', () => {
  let service: Developers;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Developers);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
