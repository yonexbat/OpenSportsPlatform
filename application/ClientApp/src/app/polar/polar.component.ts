import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { PolarRegister } from '../model/polar/polarRegister';
import { PolarService } from '../polar.service';

@Component({
  selector: 'app-polar',
  templateUrl: './polar.component.html',
  styleUrls: ['./polar.component.css']
})
export class PolarComponent implements OnInit {

  code?: string;
  polarRegister?: PolarRegister;

  constructor(private route: ActivatedRoute, private polarService: PolarService) { }

  ngOnInit(): void {   
    this.init(); 
  }

  private async init() {
    const polarRegister = await this.polarService.registerData();
    this.polarRegister = polarRegister;
    this.route.queryParams          
    .subscribe((params: Params) => {
      console.log(params);
      this.code = params['code'] as string;
      console.log(this.code);
      if(this.code) {
        this.polarService.exchangeToken({code: this.code});
      }
    });
  }

}
