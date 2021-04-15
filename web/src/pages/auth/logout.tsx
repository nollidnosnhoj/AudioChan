import { chakra } from "@chakra-ui/react";
import { useEffect } from "react";
import Router from "next/router";
import withRequiredAuth from "~/components/hoc/withRequiredAuth";
import Page from "~/components/Page";
import useUser from "~/hooks/useUser";
import { successfulToast } from "~/utils/toast";

const LogoutPage: React.FC = () => {
  const { logout } = useUser();

  useEffect(() => {
    logout()
      .then(() => {
        Router.push("/").then(() => {
          successfulToast({
            message: "You have successfully logged out.",
          });
        });
      })
      .catch((err) => console.error(err));
  }, []);

  return (
    <Page title="Logging out">
      <chakra.span>Logging out...</chakra.span>
    </Page>
  );
};

export default withRequiredAuth(LogoutPage);