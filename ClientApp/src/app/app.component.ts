import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public title: "Hoard";
  public games: {};
  steamId: string = "76561198018310420";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    console.log("here");
  }

  public fetchGames(){
    this.http.get<any>(this.baseUrl + 'api/Games/All?steamId=' + this.steamId).subscribe(result => {
      this.games = result;
      console.log(this.games);

    }, error => console.error(error));

  }

}
