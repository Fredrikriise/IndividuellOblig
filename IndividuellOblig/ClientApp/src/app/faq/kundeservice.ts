import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { FAQ, IFAQ } from "./FAQ";
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { BehaviorSubject, Observable, of as observableOf } from 'rxjs';

export class FAQNode {
    children: FAQNode[];
    FAQ: string;
    type: any;
}

@Component({
  selector: 'app-faq-component',
    templateUrl: './faq.html'
})
export class KundeService {
    visFAQ: boolean;
    FAQStatus: string;
    visFAQListe: boolean;
    alleFAQ: Array<FAQ>;
    FAQ: FormGroup;
    laster: boolean;

    nestedTreeControl: NestedTreeControl<FAQNode>;
    nestedDataSource: MatTreeNestedDataSource<FAQNode>;
    dataChange: BehaviorSubject<FAQNode[]> = new BehaviorSubject<FAQNode[]>([]);

    constructor(private _http: HttpClient) {
        this.nestedTreeControl = new NestedTreeControl<FAQNode>(this._getChildren);
        this.nestedDataSource = new MatTreeNestedDataSource();
        this.dataChange.subscribe(data => this.nestedDataSource.data = data);

        this.dataChange.next([
            {
                FAQ: "folder",
                type: "",
                children: [
                    {
                        FAQ: "test3",
                        type: "exe",
                        children: [],
                    }
                ],
            },
            {
                FAQ: "test2",
                type: "exe",
                children: [],
            },
        ]);
    }

    private _getChildren = (node: FAQNode) => { return observableOf(node.children); };
    hasNestedChild = (_: number, nodeData: FAQNode) => { return !(nodeData.type); };

    /*
    constructor(private _http: HttpClient, private fb: FormBuilder) {
        this.FAQ = fb.group({
            id: [""],
            sporsmal: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-.0-9]{5,9999}")])],
            svar: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-.0-9]{5,9999}")])]
        });
    } */

    ngOnInit() {
        this.laster = true;
        this.hentAlleFAQ();
        this.visFAQ = false;
        this.visFAQListe = true;
    }

    hentAlleFAQ() {
        this._http.get<IFAQ[]>("api/faq/")
            .subscribe(
              FAQene => {
                  this.alleFAQ = FAQene;
                  this.laster = false;
                  console.log("Ferdig med post-api/faq");
              },
              error => alert(error),
          );
    };

    onSubmit() {
        if (this.FAQStatus == "Registrer") {
            this.lagreFAQ();
        }
        else if (this.FAQStatus == "Endre") {
            this.endreEnFAQ();
        }
        else {
            alert("Feil i applikasjonen!");
        }
    }

    registrerFAQ() {
        this.FAQ.setValue({
            id: "",
            sporsmal: "",
            svar: ""
        });

        //Setter statusen til skjemaet som "uberørt" slik at det ikke blir skrevet ut valederings-feilmeldinger
        this.FAQ.markAsPristine();
        this.visFAQ = false;
        this.FAQStatus = "Registrer";
        this.visFAQ = true;
    }

    tilbakeTilFAQ() {
        this.visFAQListe = true;
        this.visFAQ = false;
    }

    lagreFAQ() {
        var lagretFAQ = new FAQ();

        lagretFAQ.sporsmal = this.FAQ.value.sporsmal;
        lagretFAQ.svar = this.FAQ.value.svar;

        const body: string = JSON.stringify(lagretFAQ);
        const headers = new HttpHeaders({ "Content-Type": "application/json" });

        this._http.post("api/faq", body, { headers: headers })
            .subscribe(
                () => {
                    this.hentAlleFAQ();
                    this.visFAQ = false;
                    this.visFAQListe = true;
                    console.log("Ferdig med post-api/faq");
                },
                error => alert(error),
            );
    };

    slettFAQ(id: number) {
        this._http.delete("api/faq/" + id)
            .subscribe(
                () => {
                    this.hentAlleFAQ();
                    console.log("Ferdig med delete-api/faq");
                },
                error => alert(error),
            );
    };

    endreFAQ(id: number) {
        this._http.get<IFAQ>("api/faq/" + id)
            .subscribe(
                faq => {
                    this.FAQ.patchValue({ id: faq.id });
                    this.FAQ.patchValue({ sporsmal: faq.sporsmal });
                    this.FAQ.patchValue({ svar: faq.svar });
                    console.log("Ferdig med get-api/faq");
                },
                error => alert(error),
        );
        this.FAQStatus = "Endre";
        this.visFAQ = true;
        this.visFAQListe = false;
    }

    endreEnFAQ() {
        const endretFAQ = new FAQ();

        endretFAQ.sporsmal = this.FAQ.value.sporsmal;
        endretFAQ.svar = this.FAQ.value.svar;

        const body: string = JSON.stringify(endretFAQ);
        const headers = new HttpHeaders({ "Content-Type": "application/json" });

        this._http.put("api/faq/" + this.FAQ.value.id, body, { headers: headers })
            .subscribe(
                () => {
                    this.hentAlleFAQ();
                    this.visFAQ = false;
                    this.visFAQListe = true;
                    console.log("Ferdig med post-api/faq");
                },
                error => alert(error)
            );
    }
}
