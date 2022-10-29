import Analyticss from "./Analyticss.css";
import { Link } from "react-router-dom";

const Analytics = () => {
  return (
    <div className="hero-image">
      <div className="hero-text">
        <h1>Ares Servis'e hoş geldiniz</h1>

        <h3>şimdi haritalarda rotaları ve yerleri keşfedin</h3>
        <Link to="/map/default">
          <button className="btn">Harita</button>
        </Link>
        <div className="icn">
          <a
            target="_blank"
            href="https://play.google.com/store/apps/details?id=com.beyhanyazilim.aresservis"
          >
            <div className="download android">
              <i className="fa fa fa-android fa-3x"></i>
              <span className="df">Download from</span>
              <span className="dfn">Google Play</span>
            </div>
          </a>
          <a
            target="_blank"
            href="https://apps.apple.com/app/ares-servis-ara%C3%A7-takip/id1639059094"
          >
            <div className="download apple">
              <i className="fa fa fa-apple fa-3x"></i>
              <span className="df">Download from</span>
              <span className="dfn">App Store</span>
            </div>
          </a>
        </div>
      </div>
    </div>
  );
};

export default Analytics;
