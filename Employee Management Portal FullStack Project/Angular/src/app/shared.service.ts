import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIurl="https://localhost:44369/api";
readonly Photourl="https://localhost:44369/Photos"

  constructor(private http:HttpClient) { }

  getDepList():Observable<any[]>{
    return this.http.get<any>(this.APIurl+'/department');
  }

  addDepartment(val:any){
    return this.http.post(this.APIurl+'/Department', val)
  }

  updateDepartment(val:any){
    return this.http.put(this.APIurl+'/Department', val)
  }

  deleteDepartment(val:any){
    return this.http.delete(this.APIurl+'/Department/'+ val)
  }

  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.APIurl+'/Employee');
  }

  addEmployee(val:any){
    return this.http.post(this.APIurl+'/Employee', val)
  }

  updateEmployee(val:any){
    return this.http.put(this.APIurl+'/Employee', val)
  }

  deleteEmployee(val:any){
    return this.http.delete(this.APIurl+'/Employee/'+val)
  }

  UploadPhoto(val:any) {
    return this.http.post(this.APIurl+'/Employee/SaveFile', val);
  }

  getAllDepartmentNames():Observable<any[]>{
    return this.http.get<any>(this.APIurl+'Employee/GetAllDepartmentNames');
  }
}
