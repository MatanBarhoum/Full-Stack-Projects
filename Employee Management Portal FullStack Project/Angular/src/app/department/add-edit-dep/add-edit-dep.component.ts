import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.css']
})
export class AddEditDepComponent implements OnInit {

  constructor(private Service:SharedService) { }

  @Input() dep:any;
  DepartmentId:string;
  DepartmentName:string;
  ngOnInit(): void {
    this.DepartmentId=this.dep.DepartmentId;
    this.DepartmentName=this.dep.DepartmentName;
  }

  updateDepartment() {
    var val = {DepartmentId:this.DepartmentId,
                DepartmentName:this.DepartmentName};
    this.Service.updateDepartment(val).subscribe(res=>{
      alert(res.toString());
    })
  }

  addDepartment() {
    var val = {DepartmentId:this.DepartmentId,
      DepartmentName:this.DepartmentName};
    this.Service.addDepartment(val).subscribe(res=>{
    alert(res.toString());
  })
}
}

