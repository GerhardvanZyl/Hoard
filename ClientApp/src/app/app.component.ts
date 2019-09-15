import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title: "Hoard";
  public games: {};

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<any>(baseUrl + 'api/Games/All').subscribe(result => {
      this.games = result;
      console.log(this.games);
    }, error => console.error(error));
  }
}
