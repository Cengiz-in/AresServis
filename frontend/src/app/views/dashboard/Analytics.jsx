import { styled, useTheme } from "@mui/material";
import Analyticss from "./Analyticss.css";
import { Link } from "react-router-dom";

const ContentBox = styled("div")(({ theme }) => ({
  margin: "30px",
  [theme.breakpoints.down("sm")]: { margin: "16px" },
}));

const Title = styled("span")(() => ({
  fontSize: "1rem",
  fontWeight: "500",
  marginRight: ".5rem",
  textTransform: "capitalize",
}));

const SubTitle = styled("span")(({ theme }) => ({
  fontSize: "0.875rem",
  color: theme.palette.text.secondary,
}));

const H4 = styled("h4")(({ theme }) => ({
  fontSize: "1rem",
  fontWeight: "500",
  marginBottom: "16px",
  textTransform: "capitalize",
  color: theme.palette.text.secondary,
}));

const Analytics = () => {
  const { palette } = useTheme();

  return (
    <div class="hero-image">
      <div class="hero-text">
        <h1>Ares Servis'e hoş geldiniz</h1>

        <h3>şimdi haritalarda rotaları ve yerleri keşfedin</h3>
        <Link to="/map/default">
          <button class="btn">Harita</button>
        </Link>
        <div class="icn">
          <a
            target="_blank"
            href="https://play.google.com/store/apps/details?id=com.beyhanyazilim.aresservis"
          >
            <div class="download android">
              <i class="fa fa fa-android fa-3x"></i>
              <span class="df">Download from</span>
              <span class="dfn">Google Play</span>
            </div>
          </a>
          <a
            target="_blank"
            href="https://apps.apple.com/app/ares-servis-ara%C3%A7-takip/id1639059094"
          >
            <div class="download apple">
              <i class="fa fa fa-apple fa-3x"></i>
              <span class="df">Download from</span>
              <span class="dfn">App Store</span>
            </div>
          </a>
        </div>
      </div>
    </div>
    // <Fragment>
    //   {/* <ContentBox className="analytics">
    //     <Grid container spacing={3}>
    //       <Grid item lg={8} md={8} sm={12} xs={12}>
    //         <StatCards />
    //         <TopSellingTable />
    //         <StatCards2 />

    //         <H4>Ongoing Projects</H4>
    //         <RowCards />
    //       </Grid>

    //       <Grid item lg={4} md={4} sm={12} xs={12}>
    //         <Card sx={{ px: 3, py: 2, mb: 3 }}>
    //           <Title>Traffic Sources</Title>
    //           <SubTitle>Last 30 days</SubTitle>

    //           <DoughnutChart
    //             height="300px"
    //             color={[palette.primary.dark, palette.primary.main, palette.primary.light]}
    //           />
    //         </Card>

    //         <UpgradeCard />
    //         <Campaigns />
    //       </Grid>
    //     </Grid>
    //   </ContentBox> */}
    // </Fragment>
  );
};

export default Analytics;
